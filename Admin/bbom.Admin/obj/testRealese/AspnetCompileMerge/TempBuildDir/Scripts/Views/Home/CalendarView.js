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
        $("#TypeId").select2();
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
    CalendarView.prototype.updateCalendar = function () {
        $('#calendar').fullCalendar('destroy');
        this.drawCalendar();
    };
    CalendarView.prototype.showEventEditor = function (event) {
        var _this = this;
        var modalButtonSubmit = $("#modal_button_submit");
        $("#model_start").val(event.start);
        if (event.hasOwnProperty("end"))
            $("#model_end").val(event.end);
        if (event.hasOwnProperty("url"))
            $("#model_url").val(event.url);
        if (event.hasOwnProperty("typeId"))
            $("#TypeId").select2().val(event.typeId).trigger("change");
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
        if (event.hasOwnProperty("title")) {
            $("#model_title").val(event.title);
            $("#myModalLabelAddEvent").text("Изменение события " + event.title);
            modalButtonSubmit.val("Изменить");
            modalButtonSubmit.attr("name", event.name);
            modalButtonSubmit.off('click');
            modalButtonSubmit.on("click", function () {
                event.title = $("#model_title").val();
                event.start = $("#model_start").val();
                event.end = $("#model_end").val();
                event.url = $("#model_url").val();
                event.typeId = $("#TypeId").select2().val();
                event.spiker = $("#model_spiker").val();
                event.icon = $("#model_icon").val();
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
        $.ajax({
            type: "POST",
            data: evnt,
            url: addEvents,
            accept: 'application/json',
            success: function (noty) {
                notify(noty);
                CalendarView.calendar.updateCalendar();
                if (CalendarView.calendar.funcUpdate != null)
                    CalendarView.calendar.funcUpdate();
            }
        });
    };
    CalendarView.prototype.addEvent = function () {
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
    function EventCalendar(title, name, url, start, end, typeId, userName, spiker, icon) {
        this.icon = icon;
        this.spiker = spiker;
        this.title = title;
        this.name = name;
        this.url = url;
        this.typeId = typeId;
        this.start = start;
        this.end = end == null || end === "" ? start : end;
        this.userName = userName;
    }
    return EventCalendar;
}());
//# sourceMappingURL=CalendarView.js.map