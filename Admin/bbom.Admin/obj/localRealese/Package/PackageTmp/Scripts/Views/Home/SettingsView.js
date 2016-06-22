var ViewObject = (function () {
    function ViewObject() {
    }
    return ViewObject;
}());
var ModalPanel = (function () {
    function ModalPanel(dropDownName, buttonName, valueName, modalLabelName, modalName, tableName, getArray, getAll, add, edit, getUserNotAdded, _delete) {
        this.dropDownName = dropDownName;
        this.buttonName = buttonName;
        this.valueName = valueName;
        this.modalLabelName = modalLabelName;
        this.modalName = modalName;
        this.tableName = tableName;
        this.getArray = getArray;
        this.getAll = getAll;
        this.add = add;
        this.edit = edit;
        this.getUserNotAdded = getUserNotAdded;
        this._delete = _delete;
    }
    return ModalPanel;
}());
var SettingsView = (function () {
    function SettingsView() {
        SettingsView.settingsView = this;
        var modalPanelCom = new ModalPanel("CommunicationId", "modal_button_submit", "model_value_com", "modalAddCommunicationLabel", "modalAddCommunication", "communicationsTable", GetCommunicationsArray, GetAllCommunications, AddCommunication, EditCommunication, GetUserNotAddedCommunications, DeleteCommunications);
        this.drawTable(modalPanelCom);
        var modalPanelExReg = new ModalPanel("ExRegParamId", "modal_button_submit_exregparam", "model_value_exreg", "modalAddExRegParamLabel", "modalAddExRegParam", "exRegTable", GetExRegParamsArray, GetAllExRegParams, AddExRegParam, EditExRegParam, GetUserNotAddedExRegParams, null);
        this.drawTable(modalPanelExReg);
    }
    SettingsView.prototype.drawTable = function (modalPanel) {
        //создание таблицы средств связи
        var table = $('#' + modalPanel.tableName).DataTable({
            pageLength: 10,
            ajax: {
                url: modalPanel.getArray,
                dataSrc: ''
            },
            dom: 'Bfrtip',
            select: true,
            buttons: [
                {
                    text: 'Добавить',
                    titleAttr: 'Добавить',
                    action: function () {
                        SettingsView.settingsView.showAdd(modalPanel);
                    }
                },
                {
                    text: 'Изменить',
                    titleAttr: 'Изменить',
                    action: function () {
                        var comRow = $('#' + modalPanel.tableName).DataTable().rows({ selected: true }).data()[0];
                        var viewObject = new ViewObject();
                        viewObject.name = comRow[1];
                        viewObject.value = comRow[2];
                        SettingsView.settingsView.showEditor(viewObject, modalPanel);
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
        if (modalPanel._delete != null)
            table.button().add(2, {
                text: 'Удалить',
                titleAttr: 'Удалить',
                action: function () {
                    var comRow = $('#' + modalPanel.tableName).DataTable().rows({ selected: true }).data()[0];
                    var viewObject = new ViewObject();
                    viewObject.objectId = comRow[0];
                    viewObject.name = comRow[1];
                    viewObject.value = comRow[2];
                    SettingsView.settingsView.delete(modalPanel, viewObject);
                }
            });
    };
    SettingsView.prototype.showEditor = function (com, modalPanel) {
        $("#" + modalPanel.dropDownName).select2({
            minimumResultsForSearch: -1,
            dropdownAutoWidth: true,
            width: 'auto',
            ajax: {
                url: modalPanel.getAll,
                dataType: 'json',
                delay: 250,
                processResults: function (data, params) {
                    return {
                        results: data
                    };
                }
            }
        });
        var modalButtonSubmit = $("#" + modalPanel.buttonName);
        //$("#CommunicationId").select2().val(com.name).trigger("change");
        $("#" + modalPanel.valueName).val(com.value);
        $("#" + modalPanel.modalLabelName).text("Изменение средства свзи: " + com.name);
        modalButtonSubmit.val("Изменить");
        modalButtonSubmit.off('click');
        modalButtonSubmit.on("click", function () {
            SettingsView.settingsView.editUser(modalPanel);
        });
        $("#" + modalPanel.modalName).modal("show");
    };
    SettingsView.prototype.showAdd = function (modalPanel) {
        $("#" + modalPanel.dropDownName).select2({
            minimumResultsForSearch: -1,
            dropdownAutoWidth: true,
            width: 'auto',
            ajax: {
                url: modalPanel.getUserNotAdded,
                dataType: 'json',
                delay: 250,
                processResults: function (data, params) {
                    return {
                        results: data
                    };
                }
            }
        });
        var modalButtonSubmit = $("#" + modalPanel.buttonName);
        modalButtonSubmit.val("Добавить");
        modalButtonSubmit.off('click');
        modalButtonSubmit.on("click", function () {
            SettingsView.settingsView.addUser(modalPanel);
        });
        $("#" + modalPanel.modalLabelName).text("Добавление средства связи");
        $("#" + modalPanel.modalName).modal("show");
    };
    SettingsView.prototype.addUser = function (modalPanel) {
        var objectId = $("#" + modalPanel.dropDownName).select2().val();
        var value = $("#" + modalPanel.valueName).val();
        var viewObject = new ViewObject();
        viewObject.objectId = objectId;
        viewObject.value = value;
        $.ajax({
            type: "POST",
            data: viewObject,
            url: modalPanel.add,
            accept: 'application/json',
            success: function (noty) {
                SettingsView.settingsView.drawSelect2(modalPanel.dropDownName, modalPanel.getUserNotAdded);
                $('#' + modalPanel.tableName).DataTable().ajax.reload();
                notify(noty);
            },
            error: function (noty) {
                notify(noty.responseJSON);
            }
        });
    };
    SettingsView.prototype.delete = function (modalPanel, viewObject) {
        $.ajax({
            type: "POST",
            data: viewObject,
            url: modalPanel._delete,
            accept: 'application/json',
            success: function (noty) {
                SettingsView.settingsView.drawSelect2(modalPanel.dropDownName, modalPanel.getUserNotAdded);
                $('#' + modalPanel.tableName).DataTable().ajax.reload();
                notify(noty);
            },
            error: function (noty) {
                notify(noty.responseJSON);
            }
        });
    };
    SettingsView.prototype.editUser = function (modalPanel) {
        var objectId = $("#" + modalPanel.dropDownName).select2().val();
        var value = $("#" + modalPanel.valueName).val();
        var viewObject = new ViewObject();
        viewObject.objectId = objectId;
        viewObject.value = value;
        viewObject.id = $('#' + modalPanel.tableName).DataTable().rows({ selected: true }).data()[0][0];
        $.ajax({
            type: "POST",
            data: viewObject,
            url: modalPanel.edit,
            accept: 'application/json',
            success: function (noty) {
                SettingsView.settingsView.drawSelect2(modalPanel.dropDownName, modalPanel.getUserNotAdded);
                $('#' + modalPanel.tableName).DataTable().ajax.reload();
                notify(noty);
            },
            error: function (noty) {
                notify(noty.responseJSON);
            }
        });
    };
    SettingsView.prototype.drawSelect2 = function (name, query) {
        $("#" + name).select2().val(null).trigger('change');
        $("#" + name).select2({
            minimumResultsForSearch: -1,
            dropdownAutoWidth: true,
            width: '300',
            ajax: {
                url: query,
                dataType: 'json',
                delay: 250,
                processResults: function (data, params) {
                    return {
                        results: data
                    };
                }
            }
        });
    };
    return SettingsView;
}());
//# sourceMappingURL=SettingsView.js.map