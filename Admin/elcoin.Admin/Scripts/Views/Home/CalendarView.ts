class CalendarView {
    funcUpdate: any;
    static calendar: CalendarView;

    public constructor(funcUpdate: any) {
        CalendarView.calendar = this;
        this.funcUpdate = funcUpdate;
        var modelStart: JQuery = $("#model_start");
        var modelEnd: JQuery = $("#model_end");

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

        $("#TypeId").select2(<any>{
            dropdownAutoWidth: true,
            width: 'auto'
        });
        $("#SpikerId").select2(<any>{
            dropdownAutoWidth: true,
            width: 'auto'
        });
    }

    private drawCalendar() {
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

    private createCalendar(name: string, editavle: boolean, data: any): void {
        if (editavle)
            $('#' + name).fullCalendar({
                eventDrop(event, delta, revertFunc) {
                    CalendarView.calendar.getEventById(event.name, e => {
                        e.start = event.start.format();
                        e.end = event.end == null ? event.start.format() : event.end.format();
                        EventActions.editEvent(e,
                            () => {
                                if (CalendarView.calendar.funcUpdate != null)
                                    CalendarView.calendar.funcUpdate();
                                revertFunc();
                            },
                            () => {
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

    public getEventById(id: string, successCallback: (e: EventCalendar, rev?: any) => void) {
        $.ajax({
            type: "GET",
            url: getEventJsonEvents + "?id=" + id,
            success: successCallback,
            error(noty) {
                notify(noty.responseJSON);
            }
        });
    }

    public updateCalendar() {
        $('#calendar').fullCalendar('destroy');
        this.drawCalendar();
    }

    private showEventEditor(event: EventCalendar) {
        var eventButtons = new EventButtons();
        eventButtons.modalButtonStart = $("#startEvent");
        eventButtons.modalButtonStop = $("#stopEvent");
        eventButtons.modalButtonUpRoles = $("#upRoles");
        eventButtons.modalEventStatus = $("#recStatus");
        eventButtons.init(event);
        this.fillModal(event);
        $("#modalAddEvent").modal("show");
    }

    private fillModal(event: EventCalendar) {
        var modalButtonSubmit: JQuery = $("#modal_button_submit");
        $("#model_start").val(event.start);
        $("#model_end").val(event.end);
        $("#model_url").val(event.url);
        if (event.typeId != null)
            $("#TypeId").select2(<any>{
                dropdownAutoWidth: true,
                width: 'auto'
            }).val(event.typeId).trigger("change");
        if (event.spikerId != null)
            $("#SpikerId").select2(<any>{
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
            EventActions.editEvent(event,
                () => {
                    if (CalendarView.calendar.funcUpdate != null)
                        CalendarView.calendar.funcUpdate();
                },
                () => {
                    CalendarView.calendar.updateCalendar();

                });
        });
    }

    public addEvent() {
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
            var evnt: EventCalendar = new EventCalendar(title, null, url, start, end, typeId);
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
    title: string;
    name: string;
    url: string;
    typeId: string;
    spikerId: string;
    start: string;
    end: string;
    userName: string;
    spiker: string;
    icon: string;
    description: string;
    status: string;

    constructor(title: string,
        name: string,
        url: string,
        start: string,
        end: string,
        typeId?: string,
        spikerId?: string,
        userName?: string,
        spiker?: string,
        icon?: string,
        description?: string) {
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
    private static execute(m: string, data: EventCalendar, url: string, succesCallback?: () => void, errorCallback?: () => void) {
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

    static startEvent(e: EventCalendar) {
        this.execute("POST", e, startEvents);
    }

    static stopEvent(e: EventCalendar) {
        this.execute("POST", e, stopEvents);
    }

    static upRoles(e: EventCalendar) {
        this.execute("POST", e, upRolesEvents);
    }


    static createEvent(e: EventCalendar, callback: () => void) {
        this.execute("POST", e, addEvents, callback);
    }


    static editEvent(e: EventCalendar, scallback: () => void, ecallback: () => void) {
        this.execute("POST", e, editEvents, scallback, ecallback);
    }
}

class EventButtons {
    public static thisEvBt: EventButtons;
    modalButtonStart: JQuery;
    modalButtonStop: JQuery;
    modalEventStatus: JQuery;
    modalButtonUpRoles: JQuery;

    constructor() {
        EventButtons.thisEvBt = this;
    }

    initByEventId(id: number, successCallback?: () => void): void {
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

    init(event: EventCalendar): void {
        if (event.status != null) {
            if (event.status === "1") {
                this.startAction();
            } else {
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

    private startAction(): void {
        this.modalEventStatus.attr("fill", "red");
        this.modalButtonStop.removeClass('disabled');
        this.modalButtonStart.addClass('disabled');
    }

    private stopAction(): void {
        this.modalEventStatus.attr("fill", "white");
        this.modalButtonStart.removeClass('disabled');
        this.modalButtonStop.addClass('disabled');
    }

    visible(v: boolean): void {
        if (v) {
            this.modalButtonStart.css("visibility", "visible");
            this.modalButtonStop.css("visibility", "visible");
            this.modalButtonUpRoles.css("visibility", "visible");
        } else {
            this.modalButtonStart.css("visibility", "hidden");
            this.modalButtonStop.css("visibility", "hidden");
            this.modalButtonUpRoles.css("visibility", "hidden");
            this.modalEventStatus.attr("fill", "white");
        }
    }
}