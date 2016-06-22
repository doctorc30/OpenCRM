class TableButton {
    constructor(link, name, needId, inRow) {
        this.link = link;
        this.name = name;
        this.needId = needId;
        this.inRow = inRow;
    }
}
class TableButtonGroup {
}
class TableButtonInRow extends TableButton {
    constructor(link, name, needId, inRow, _class, ajax) {
        super(link, name, needId, true);
        this._class = _class;
        this.ajax = ajax;
    }
}
class TableModel {
}
class TableActions {
    static redirectToAction(link) {
        window.location = (link);
    }
    static redirectToActionById(link, tableName, target) {
        var id = $('#' + tableName).DataTable().row($(target).parents('tr')).data()[0];
        var newLink = link + "/" + id;
        TableActions.redirectToAction(newLink);
    }
}
class TableGenerator {
    constructor(tableParent, table, callback) {
        this.tableParent = tableParent;
        TableGenerator.tableGenerator = this;
        $.ajax({
            type: "GET",
            url: "/Tables/GetTableModel" + "?table=" + table,
            success(json) {
                TableGenerator.tableGenerator.tableModel = json;
                TableGenerator.tableGenerator.generateTableTemplate();
                TableGenerator.tableGenerator.generateTable();
                TableGenerator.tableGenerator.bindEvents();
                if (callback != null) {
                    callback(TableGenerator.tableGenerator.tableModel);
                }
            }
        });
    }
    generateTableTemplate() {
        var tableparent = $('#' + this.tableParent);
        var table = $('<table id="' + this.tableModel.name + '"></table>');
        var thead = $('<thead></thead>');
        var tr = $('<tr></tr>');
        for (var i = 0; i < this.tableModel.columns.length; i++) {
            tr.append('<th>' + this.tableModel.columns[i] + '</th>');
        }
        thead.append(tr);
        table.append(thead);
        tableparent.append(table);
    }
    generateTable() {
        var buutons = this.generateButtonsObject();
        var columnDefs = this.generateColumnDefsObject();
        $('#' + this.tableModel.name)
            .DataTable({
            pageLength: 10,
            ajax: {
                url: this.tableModel.link,
                dataSrc: ''
            },
            dom: 'Bfrtip',
            select: this.tableModel.select,
            buttons: buutons,
            columnDefs: columnDefs
        });
    }
    generateButtonsObject() {
        var buttons = [];
        for (var i = 0; i < this.tableModel.buttons.length; i++) {
            if (!this.tableModel.buttons[i].inRow) {
                var button = {};
                button.text = this.tableModel.buttons[i].name;
                button.titleAttr = this.tableModel.buttons[i].name;
                var link;
                if (!this.tableModel.buttons[i].needId) {
                    link = this.tableModel.buttons[i].link;
                    button.action = () => { TableActions.redirectToAction(link); };
                }
                else {
                    link = this.tableModel.buttons[i].link;
                    button.action = () => { TableActions.redirectToActionById(link, this.tableModel.name, null); };
                }
                ;
                buttons.push(button);
            }
        }
        return buttons;
    }
    generateColumnDefsObject() {
        var columnDefs = [];
        try {
            this.classButtonsInRow = new Map();
            for (var i = 0; i < this.tableModel.buttonsInRow.length; i++) {
                var columnDef = {};
                columnDef.targets = this.tableModel.buttonsInRow[i].columnIndex;
                columnDef.defaultContent = "";
                columnDef.data = null;
                for (var j = 0; j < this.tableModel.buttonsInRow[i].buttons.length; j++) {
                    var btnInRow = this.tableModel.buttonsInRow[i].buttons[j];
                    var btn = "<a class=\"btn btn-primary tablBtn " +
                        btnInRow._class +
                        "\" style=\"font-size: 13px\">" +
                        btnInRow.name +
                        "</a>";
                    columnDef.defaultContent += btn;
                    this.classButtonsInRow.set(btnInRow._class, btnInRow);
                }
                if (columnDef.defaultContent !== "") {
                    columnDefs.push(columnDef);
                }
            }
        }
        catch (e) {
        }
        return columnDefs;
    }
    bindEvents() {
        this.classButtonsInRow.forEach((value, index, map) => {
            $("#" + this.tableModel.name + " tbody")
                .on('click', '.' + index, (ev) => {
                if (!value.needId) {
                    TableActions.redirectToAction(value.link);
                }
                else {
                    TableActions.redirectToActionById(value.link, this.tableModel.name, ev.target);
                }
                ;
            });
        });
    }
}
//# sourceMappingURL=TableGenerator.js.map