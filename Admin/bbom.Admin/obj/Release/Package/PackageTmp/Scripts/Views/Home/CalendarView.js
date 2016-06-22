var CalendarView = (function () {
    function CalendarView(funcUpdate) {
        CalendarView.calendar = this;
        this.funcUpdate = funcUpdate;
        var modelStart = $("#model_start");
        var modelEnd = $("#model_end");
        modelStart.datetimepicker({
            locale: 'ru',
            format: "YYYY-MM-D HH:mm"
        });
        modelEnd.datetimepicker({
            locale: 'ru',
            format: "YYYY-MM-D HH:mm"
        });
        this.drawCalendar();
        if (!userIsRole("admin")) {
            $("#addEventButton").css("visibility", "collapse");
        }
        $("#TypeId").select2({
            dropdownAutoWidth: true,
            width: 'auto'
        });
        $("#SpikerId").select2({
            dropdownAutoWidth: true,
            width: 'auto'
        });
    }
    CalendarView.prototype.drawCalendar = function () {
        $.ajax({
            type: "GET",
            url: getAllJsonEvents,
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    var url = data[i].url;
                    data[i].video = url;
                    data[i].url = indexEvents + "?id=" + data[i].name;
                }
                if (userIsRole("admin"))
                    CalendarView.calendar.createCalendar('calendar', true, data);
                else {
                    CalendarView.calendar.createCalendar('calendar', false, data);
                }
            }
        });
    };
    CalendarView.prototype.createCalendar = function (name, editavle, data) {
        if (editavle)
            $('#' + name).fullCalendar({
                eventDrop: function (event, delta, revertFunc) {
                    CalendarView.calendar.getEventById(event.name, function (e) {
                        e.start = event.start.format();
                        e.end = event.end == null ? event.start.format() : event.end.format();
                        CalendarView.calendar.editEvent(e, revertFunc);
                    });
                },
                eventRightclick: function (event) {
                    CalendarView.calendar.getEventById(event.name, function (e) {
                        CalendarView.calendar.showEventEditor(e);
                    });
                },
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },
                lang: 'ru',
                timeFormat: 'H(:mm)',
                editable: editavle,
                events: data
            });
        else {
            $('#' + name).fullCalendar({
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },
                lang: 'ru',
                timeFormat: 'H(:mm)',
                editable: editavle,
                events: data
            });
        }
    };
    CalendarView.prototype.getEventById = function (id, successCallback) {
        $.ajax({
            type: "GET",
            url: getEventJsonEvents + "?id=" + id,
            success: successCallback,
            error: function (noty) {
                notify(noty.responseJSON);
            }
        });
    };
    CalendarView.prototype.editEvent = function (event, revertFunc) {
        $("#modalAddEvent").modal("hide");
        $.ajax({
            type: "POST",
            data: event,
            url: editEvents,
            accept: "application/json",
            success: function (noty) {
                notify(noty);
                if (CalendarView.calendar.funcUpdate != null)
                    CalendarView.calendar.funcUpdate();
                if (revertFunc === null)
                    CalendarView.calendar.updateCalendar();
            },
            error: function (noty) {
                notify(noty.responseJSON);
                if (revertFunc != null)
                    revertFunc();
                CalendarView.calendar.updateCalendar();
            }
        });
        $("#modalAddEvent").modal("hide");
    };
    CalendarView.prototype.startEvent = function (event) {
        $("#modalAddEvent").modal("hide");
        $.ajax({
            type: "POST",
            data: event,
            url: startEvents,
            accept: "application/json",
            success: function (noty) {
                notify(noty);
            },
            error: function (noty) {
                notify(noty.responseJSON);
            }
        });
        $("#modalAddEvent").modal("hide");
    };
    CalendarView.prototype.stopEvent = function (event) {
        $("#modalAddEvent").modal("hide");
        $.ajax({
            type: "POST",
            data: event,
            url: stopEvents,
            accept: "application/json",
            success: function (noty) {
                notify(noty);
            },
            error: function (noty) {
                notify(noty.responseJSON);
            }
        });
        $("#modalAddEvent").modal("hide");
    };
    CalendarView.prototype.upRoles = function (event) {
        $("#modalAddEvent").modal("hide");
        $.ajax({
            type: "POST",
            data: event,
            url: upRolesEvents,
            accept: "application/json",
            success: function (noty) {
                notify(noty);
            },
            error: function (noty) {
                notify(noty.responseJSON);
            }
        });
        $("#modalAddEvent").modal("hide");
    };
    CalendarView.prototype.updateCalendar = function () {
        $('#calendar').fullCalendar('destroy');
        this.drawCalendar();
    };
    CalendarView.prototype.showEventEditor = function (event) {
        var _this = this;
        var modalButtonSubmit = $("#modal_button_submit");
        var modalButtonStart = $("#startEvent");
        var modalButtonStop = $("#stopEvent");
        var modalButtonUpRoles = $("#upRoles");
        var modalEventStatus = $("#recStatus");
        $("#model_start").val(event.start);
        if (event.hasOwnProperty("end"))
            $("#model_end").val(event.end);
        if (event.hasOwnProperty("url"))
            $("#model_url").val(event.url);
        if (event.hasOwnProperty("typeId"))
            $("#TypeId").select2({
                dropdownAutoWidth: true,
                width: 'auto'
            }).val(event.typeId).trigger("change");
        if (event.hasOwnProperty("spikerId"))
            $("#SpikerId").select2({
                dropdownAutoWidth: true,
                width: 'auto'
            }).val(event.spikerId).trigger("change");
        if (event.hasOwnProperty("userName")) {
            $("#model_user").text(event.userName);
            $("#userPanel").css("visibility", "visible");
        }
        if (event.hasOwnProperty("spiker")) {
            $("#model_spiker").val(event.spiker);
        }
        if (event.hasOwnProperty("icon")) {
            $("#model_icon").val(event.icon);
        }
        if (event.hasOwnProperty("status")) {
            if (event.status === "1") {
                modalEventStatus.attr("fill", "red");
                modalButtonStop.removeClass('disabled');
                modalButtonStart.addClass('disabled');
            }
            else {
                modalEventStatus.attr("fill", "white");
                modalButtonStart.removeClass('disabled');
                modalButtonStop.addClass('disabled');
            }
        }
        if (event.hasOwnProperty("title")) {
            $("#model_title").val(event.title);
            $("#myModalLabelAddEvent").text("Изменение события " + event.title);
            modalButtonStart.css("visibility", "visible");
            modalButtonStop.css("visibility", "visible");
            modalButtonUpRoles.css("visibility", "visible");
            modalButtonSubmit.val("Изменить");
            modalButtonSubmit.attr("name", event.name);
            modalButtonStart.off('click');
            modalButtonStart.on("click", function () {
                CalendarView.calendar.startEvent(event);
            });
            modalButtonUpRoles.off('click');
            modalButtonUpRoles.on("click", function () {
                CalendarView.calendar.upRoles(event);
            });
            modalButtonStop.off('click');
            modalButtonStop.on("click", function () {
                CalendarView.calendar.stopEvent(event);
            });
            modalButtonSubmit.off('click');
            modalButtonSubmit.on("click", function () {
                event.title = $("#model_title").val();
                event.start = $("#model_start").val();
                event.end = $("#model_end").val();
                event.url = $("#model_url").val();
                event.typeId = $("#TypeId").select2().val();
                event.spikerId = $("#SpikerId").select2().val();
                event.spiker = $("#model_spiker").val();
                event.icon = $("#model_icon").val();
                event.description = $("#model_des").val();
                _this.editEvent(event, null);
            });
        }
        $("#modalAddEvent").modal("show");
    };
    CalendarView.prototype.createEvent = function () {
        var title = $("#model_title").val();
        var start = $("#model_start").val();
        var end = $("#model_end").val();
        var url = $("#model_url").val();
        var typeId = $("#TypeId").select2().val();
        var evnt = new EventCalendar(title, null, url, start, end, typeId);
        evnt.spiker = $("#model_spiker").val();
        evnt.icon = $("#model_icon").val();
        evnt.description = $("#model_des").val();
        evnt.spikerId = $("#SpikerId").select2().val();
        $.ajax({
            type: "POST",
            data: evnt,
            url: addEvents,
            accept: 'application/json',
            success: function (noty) {
                notify(noty);
                $("#modalAddEvent").modal("hide");
                CalendarView.calendar.updateCalendar();
                if (CalendarView.calendar.funcUpdate != null)
                    CalendarView.calendar.funcUpdate();
            }
        });
    };
    CalendarView.prototype.addEvent = function () {
        var modalButtonStart = $("#startEvent");
        var modalButtonStop = $("#stopEvent");
        var modalEventStatus = $("#recStatus");
        var modalButtonUpRoles = $("#upRoles");
        modalButtonStart.css("visibility", "hidden");
        modalButtonUpRoles.css("visibility", "hidden");
        modalButtonStop.css("visibility", "hidden");
        modalEventStatus.attr("fill", "white");
        $("#modal_button_submit").val("Добавить");
        $("#modal_button_submit").off('click');
        $("#modal_button_submit").on("click", function () {
            CalendarView.calendar.createEvent();
        });
        $("#myModalLabelAddEvent").text("Новое событие");
        $("#model_start").val('');
        $("#model_end").val('');
        $("#modalAddEvent").modal("show");
    };
    return CalendarView;
}());
var EventCalendar = (function () {
    function EventCalendar(title, name, url, start, end, typeId, spikerId, userName, spiker, icon, description) {
        this.icon = icon;
        this.spiker = spiker;
        this.title = title;
        this.name = name;
        this.url = url;
        this.typeId = typeId;
        this.spikerId = spikerId;
        this.start = start;
        this.end = end == null || end === "" ? start : end;
        this.userName = userName;
        this.description = description;
    }
    return EventCalendar;
}());
//# sourceMappingURL=CalendarView.js.map