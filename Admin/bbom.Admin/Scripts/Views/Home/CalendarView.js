class CalendarView {
    constructor(funcUpdate) {
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
    drawCalendar() {
        $.ajax({
            type: "GET",
            url: getAllJsonEvents,
            success(data) {
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
    }
    createCalendar(name, editavle, data) {
        if (editavle)
            $('#' + name).fullCalendar({
                eventDrop(event, delta, revertFunc) {
                    CalendarView.calendar.getEventById(event.name, e => {
                        e.start = event.start.format();
                        e.end = event.end == null ? event.start.format() : event.end.format();
                        EventActions.editEvent(e, null, () => {
                            CalendarView.calendar.updateCalendar();
                            revertFunc();
                        });
                    });
                },
                eventRightclick(event) {
                    CalendarView.calendar.getEventById(event.name, e => {
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
    }
    getEventById(id, successCallback) {
        $.ajax({
            type: "GET",
            url: getEventJsonEvents + "?id=" + id,
            success: successCallback,
            error(noty) {
                notify(noty.responseJSON);
            }
        });
    }
    updateCalendar() {
        $('#calendar').fullCalendar('destroy');
        this.drawCalendar();
    }
    showEventEditor(event) {
        var eventButtons = new EventButtons();
        eventButtons.modalButtonStart = $("#startEvent");
        eventButtons.modalButtonStop = $("#stopEvent");
        eventButtons.modalButtonUpRoles = $("#upRoles");
        eventButtons.modalEventStatus = $("#recStatus");
        eventButtons.init(event);
        this.fillModal(event);
        $("#modalAddEvent").modal("show");
    }
    fillModal(event) {
        var modalButtonSubmit = $("#modal_button_submit");
        $("#model_start").val(event.start);
        $("#model_end").val(event.end);
        $("#model_url").val(event.url);
        if (event.typeId != null)
            $("#TypeId").select2({
                dropdownAutoWidth: true,
                width: 'auto'
            }).val(event.typeId).trigger("change");
        if (event.spikerId != null)
            $("#SpikerId").select2({
                dropdownAutoWidth: true,
                width: 'auto'
            }).val(event.spikerId).trigger("change");
        if (event.userName != null) {
            $("#model_user").text(event.userName);
            $("#userPanel").css("visibility", "visible");
        }
        if (event.spiker != null) {
            $("#model_spiker").val(event.spiker);
        }
        if (event.icon != null) {
            $("#model_icon").val(event.icon);
        }
        $("#model_title").val(event.title);
        $("#myModalLabelAddEvent").text("Изменение события " + event.title);
        modalButtonSubmit.val("Изменить");
        modalButtonSubmit.attr("name", event.name);
        modalButtonSubmit.off('click');
        modalButtonSubmit.on("click", () => {
            event.title = $("#model_title").val();
            event.start = $("#model_start").val();
            event.end = $("#model_end").val();
            event.url = $("#model_url").val();
            event.typeId = $("#TypeId").select2().val();
            event.spikerId = $("#SpikerId").select2().val();
            event.spiker = $("#model_spiker").val();
            event.icon = $("#model_icon").val();
            event.description = $("#model_des").val();
            EventActions.editEvent(event, () => {
                if (CalendarView.calendar.funcUpdate != null)
                    CalendarView.calendar.funcUpdate();
            }, () => {
                CalendarView.calendar.updateCalendar();
            });
        });
    }
    addEvent() {
        var eventButtons = new EventButtons();
        eventButtons.modalButtonStart = $("#startEvent");
        eventButtons.modalButtonStop = $("#stopEvent");
        eventButtons.modalButtonUpRoles = $("#upRoles");
        eventButtons.modalEventStatus = $("#recStatus");
        eventButtons.visible(false);
        var modalButtonSubmit = $("#modal_button_submit");
        modalButtonSubmit.val("Добавить");
        modalButtonSubmit.off('click');
        modalButtonSubmit.on("click", () => {
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
            EventActions.createEvent(evnt, () => {
                $("#modalAddEvent").modal("hide");
                CalendarView.calendar.updateCalendar();
                if (CalendarView.calendar.funcUpdate != null)
                    CalendarView.calendar.funcUpdate();
            });
        });
        $("#myModalLabelAddEvent").text("Новое событие");
        $("#model_start").val('');
        $("#model_end").val('');
        $("#modalAddEvent").modal("show");
    }
}
class EventCalendar {
    constructor(title, name, url, start, end, typeId, spikerId, userName, spiker, icon, description) {
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
}
class EventActions {
    static execute(m, data, url, succesCallback, errorCallback) {
        $.ajax({
            type: m,
            data: data,
            url: url,
            accept: "application/json",
            success(noty) {
                notify(noty);
                if (succesCallback != null)
                    succesCallback();
                $("#modalAddEvent").modal("hide");
            },
            error(noty) {
                notify(noty.responseJSON);
                if (errorCallback != null)
                    errorCallback();
                $("#modalAddEvent").modal("hide");
            }
        });
    }
    static startEvent(e) {
        this.execute("POST", e, startEvents);
    }
    static stopEvent(e) {
        this.execute("POST", e, stopEvents);
    }
    static upRoles(e) {
        this.execute("POST", e, upRolesEvents);
    }
    static createEvent(e, callback) {
        this.execute("POST", e, addEvents, callback);
    }
    static editEvent(e, scallback, ecallback) {
        this.execute("POST", e, editEvents, scallback, ecallback);
    }
}
class EventButtons {
    constructor() {
        EventButtons.thisEvBt = this;
    }
    initByEventId(id, successCallback) {
        $.ajax({
            type: "GET",
            url: getEventJsonEvents + "?id=" + id,
            success(e) {
                EventButtons.thisEvBt.init(e);
                if (successCallback != null) {
                    successCallback();
                }
            },
            error(noty) {
                notify(noty.responseJSON);
            }
        });
    }
    init(event) {
        if (event.status != null) {
            if (event.status === "1") {
                this.startAction();
            }
            else {
                this.stopAction();
            }
        }
        this.visible(true);
        this.modalButtonStart.off('click');
        this.modalButtonStart.on("click", () => {
            EventActions.startEvent(event);
            this.startAction();
        });
        this.modalButtonUpRoles.off('click');
        this.modalButtonUpRoles.on("click", () => {
            EventActions.upRoles(event);
        });
        this.modalButtonStop.off('click');
        this.modalButtonStop.on("click", () => {
            EventActions.stopEvent(event);
            this.stopAction();
        });
    }
    startAction() {
        this.modalEventStatus.attr("fill", "red");
        this.modalButtonStop.removeClass('disabled');
        this.modalButtonStart.addClass('disabled');
    }
    stopAction() {
        this.modalEventStatus.attr("fill", "white");
        this.modalButtonStart.removeClass('disabled');
        this.modalButtonStop.addClass('disabled');
    }
    visible(v) {
        if (v) {
            this.modalButtonStart.css("visibility", "visible");
            this.modalButtonStop.css("visibility", "visible");
            this.modalButtonUpRoles.css("visibility", "visible");
        }
        else {
            this.modalButtonStart.css("visibility", "hidden");
            this.modalButtonStop.css("visibility", "hidden");
            this.modalButtonUpRoles.css("visibility", "hidden");
            this.modalEventStatus.attr("fill", "white");
        }
    }
}
//# sourceMappingURL=CalendarView.js.map