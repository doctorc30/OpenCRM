var Communication = (function () {
    function Communication() {
    }
    return Communication;
}());
var SettingsView = (function () {
    function SettingsView() {
        SettingsView.settingsView = this;
        this.drawCommunicationsTable();
    }
    SettingsView.prototype.drawCommunicationsTable = function () {
        $('#communicationsTable').DataTable({
            pageLength: 10,
            ajax: {
                url: GetCommunicationsArray,
                dataSrc: ''
            },
            dom: 'Bfrtip',
            select: true,
            buttons: [
                {
                    text: 'Добавить',
                    titleAttr: 'Добавить',
                    action: function () {
                        SettingsView.settingsView.showCommunicationAdd();
                    }
                },
                {
                    text: 'Изменить',
                    titleAttr: 'Изменить',
                    action: function () {
                        var comRow = $('#communicationsTable').DataTable().rows({ selected: true }).data()[0];
                        var com = new Communication();
                        com.name = comRow[1];
                        com.value = comRow[2];
                        SettingsView.settingsView.showCommunicationsEditor(com);
                    }
                }
            ],
            columnDefs: [
                {
                    targets: [0],
                    visible: false,
                    searchable: false
                }
            ]
        });
    };
    SettingsView.prototype.showCommunicationsEditor = function (com) {
        $("#CommunicationId").select2({
            ajax: {
                url: GetAllCommunications,
                dataType: 'json',
                delay: 250,
                processResults: function (data, params) {
                    return {
                        results: data
                    };
                }
            }
        });
        var modalButtonSubmit = $("#modal_button_submit");
        //$("#CommunicationId").select2().val(com.name).trigger("change");
        $("#model_value").val(com.value);
        $("#myModalLabelAddEvent").text("Изменение средства свзи: " + com.name);
        modalButtonSubmit.val("Изменить");
        modalButtonSubmit.off('click');
        modalButtonSubmit.on("click", function () {
            SettingsView.settingsView.editUserCommunication();
        });
        $("#modalAddCommunication").modal("show");
    };
    SettingsView.prototype.showCommunicationAdd = function () {
        $("#CommunicationId").select2({
            ajax: {
                url: GetUserNotAddedCommunications,
                dataType: 'json',
                delay: 250,
                processResults: function (data, params) {
                    return {
                        results: data
                    };
                }
            }
        });
        $("#modal_button_submit").val("Добавить");
        $("#modal_button_submit").off('click');
        $("#modal_button_submit").on("click", function () {
            SettingsView.settingsView.addUserCommunication();
        });
        $("#modalAddCommunicationLabel").text("Добавление средства связи");
        $("#modalAddCommunication").modal("show");
    };
    SettingsView.prototype.addUserCommunication = function () {
        var communicationId = $("#CommunicationId").select2().val();
        var value = $("#model_value").val();
        var com = new Communication();
        com.communicationId = communicationId;
        com.value = value;
        $.ajax({
            type: "POST",
            data: com,
            url: AddCommunication,
            accept: 'application/json',
            success: function (noty) {
                $('#communicationsTable').DataTable().ajax.reload();
                notify(noty);
            }
        });
    };
    SettingsView.prototype.editUserCommunication = function () {
        var communicationId = $("#CommunicationId").select2().val();
        var value = $("#model_value").val();
        var com = new Communication();
        com.communicationId = communicationId;
        com.value = value;
        com.id = $('#communicationsTable').DataTable().rows({ selected: true }).data()[0][0];
        $.ajax({
            type: "POST",
            data: com,
            url: EditCommunication,
            accept: 'application/json',
            success: function (noty) {
                $('#communicationsTable').DataTable().ajax.reload();
                notify(noty);
            }
        });
    };
    return SettingsView;
}());
//# sourceMappingURL=SettingsView.js.map