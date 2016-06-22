class ViewObject {
}
class ModalPanel {
    constructor(dropDownName, buttonName, valueName, modalLabelName, modalName, tableName, getArray, getAll, add, edit, getUserNotAdded, _delete) {
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
}
class SettingsView {
    constructor() {
        SettingsView.settingsView = this;
        var modalPanelCom = new ModalPanel("CommunicationId", "modal_button_submit", "model_value_com", "modalAddCommunicationLabel", "modalAddCommunication", "communicationsTable", GetCommunicationsArray, GetAllCommunications, AddCommunication, EditCommunication, GetUserNotAddedCommunications, DeleteCommunications);
        this.drawTable(modalPanelCom);
        var modalPanelExReg = new ModalPanel("ExRegParamId", "modal_button_submit_exregparam", "model_value_exreg", "modalAddExRegParamLabel", "modalAddExRegParam", "exRegTable", GetExRegParamsArray, GetAllExRegParams, AddExRegParam, EditExRegParam, GetUserNotAddedExRegParams, null);
        this.drawTable(modalPanelExReg);
    }
    drawTable(modalPanel) {
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
                    action() {
                        SettingsView.settingsView.showAdd(modalPanel);
                    }
                },
                {
                    text: 'Изменить',
                    titleAttr: 'Изменить',
                    action() {
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
                action() {
                    var comRow = $('#' + modalPanel.tableName).DataTable().rows({ selected: true }).data()[0];
                    var viewObject = new ViewObject();
                    viewObject.objectId = comRow[0];
                    viewObject.name = comRow[1];
                    viewObject.value = comRow[2];
                    SettingsView.settingsView.delete(modalPanel, viewObject);
                }
            });
    }
    showEditor(com, modalPanel) {
        $("#" + modalPanel.dropDownName).select2({
            minimumResultsForSearch: -1,
            dropdownAutoWidth: true,
            width: 'auto',
            ajax: {
                url: modalPanel.getAll,
                dataType: 'json',
                delay: 250,
                processResults(data, params) {
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
        modalButtonSubmit.on("click", () => {
            SettingsView.settingsView.editUser(modalPanel);
        });
        $("#" + modalPanel.modalName).modal("show");
    }
    showAdd(modalPanel) {
        $("#" + modalPanel.dropDownName).select2({
            minimumResultsForSearch: -1,
            dropdownAutoWidth: true,
            width: 'auto',
            ajax: {
                url: modalPanel.getUserNotAdded,
                dataType: 'json',
                delay: 250,
                processResults(data, params) {
                    return {
                        results: data
                    };
                }
            }
        });
        var modalButtonSubmit = $("#" + modalPanel.buttonName);
        modalButtonSubmit.val("Добавить");
        modalButtonSubmit.off('click');
        modalButtonSubmit.on("click", () => {
            SettingsView.settingsView.addUser(modalPanel);
        });
        $("#" + modalPanel.modalLabelName).text("Добавление средства связи");
        $("#" + modalPanel.modalName).modal("show");
    }
    addUser(modalPanel) {
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
            success(noty) {
                SettingsView.settingsView.drawSelect2(modalPanel.dropDownName, modalPanel.getUserNotAdded);
                $('#' + modalPanel.tableName).DataTable().ajax.reload();
                notify(noty);
            },
            error(noty) {
                notify(noty.responseJSON);
            }
        });
    }
    delete(modalPanel, viewObject) {
        $.ajax({
            type: "POST",
            data: viewObject,
            url: modalPanel._delete,
            accept: 'application/json',
            success(noty) {
                SettingsView.settingsView.drawSelect2(modalPanel.dropDownName, modalPanel.getUserNotAdded);
                $('#' + modalPanel.tableName).DataTable().ajax.reload();
                notify(noty);
            },
            error(noty) {
                notify(noty.responseJSON);
            }
        });
    }
    editUser(modalPanel) {
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
            success(noty) {
                SettingsView.settingsView.drawSelect2(modalPanel.dropDownName, modalPanel.getUserNotAdded);
                $('#' + modalPanel.tableName).DataTable().ajax.reload();
                notify(noty);
            },
            error(noty) {
                notify(noty.responseJSON);
            }
        });
    }
    drawSelect2(name, query) {
        $("#" + name).select2().val(null).trigger('change');
        $("#" + name).select2({
            minimumResultsForSearch: -1,
            dropdownAutoWidth: true,
            width: '300',
            ajax: {
                url: query,
                dataType: 'json',
                delay: 250,
                processResults(data, params) {
                    return {
                        results: data
                    };
                }
            }
        });
    }
}
//# sourceMappingURL=SettingsView.js.map