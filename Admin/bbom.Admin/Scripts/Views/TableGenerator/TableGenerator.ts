class TableButton {
    constructor(link: string, name: string, needId: boolean, inRow: boolean) {
        this.link = link;
        this.name = name;
        this.needId = needId;
        this.inRow = inRow;
    }

    link: string;
    name: string;
    needId: boolean;
    inRow: boolean;
}

class TableButtonGroup {
    buttons: TableButtonInRow[];
    columnIndex: number;
    style: any;
}

class TableButtonInRow extends TableButton {
    constructor(link: string, name: string, needId: boolean, inRow: boolean, _class: string, ajax: boolean) {
        super(link, name, needId, true);
        this._class = _class;
        this.ajax = ajax;
    }

    _class: string;
    ajax: boolean;
}

class TableModel {
    buttons: TableButton[];
    buttonsInRow: TableButtonGroup[];
    columns: string[];
    link: string;
    name: string;
    select: boolean;
}

class TableActions {
    public static redirectToAction(link: string) {
        window.location = ((link) as any);
    }

    public static redirectToActionById(link: string, tableName: string, target: Element) {
        var id = $('#' + tableName).DataTable().row($(target).parents('tr')).data()[0];
        var newLink = link + "/" + id;
        TableActions.redirectToAction(newLink);
    }
}

class TableGenerator {
    public static tableGenerator: TableGenerator;
    classButtonsInRow: Map<string, TableButtonInRow>;
    tableParent: string;
    tableModel: TableModel;

    constructor(tableParent: string, table: string, callback?: (tableModel: TableModel)=>void) {
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

    private generateTableTemplate() {
        var tableparent: JQuery = $('#' + this.tableParent);
        var table: JQuery = $('<table id="' + this.tableModel.name + '"></table>');
        var thead: JQuery = $('<thead></thead>');
        var tr: JQuery = $('<tr></tr>');
        for (var i = 0; i < this.tableModel.columns.length; i++) {
            tr.append('<th>' + this.tableModel.columns[i] + '</th>');
        }
        thead.append(tr);
        table.append(thead);
        tableparent.append(table);
    }

    private generateTable() {
        var buutons = this.generateButtonsObject();
        var columnDefs = this.generateColumnDefsObject();
        $('#' + this.tableModel.name)
            .DataTable(<any>{
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

    private generateButtonsObject(): any {
        var buttons = [];
        for (var i = 0; i < this.tableModel.buttons.length; i++) {
            if (!this.tableModel.buttons[i].inRow) {
                var button: any = {};
                button.text = this.tableModel.buttons[i].name;
                button.titleAttr = this.tableModel.buttons[i].name;
                var link: string;
                if (!this.tableModel.buttons[i].needId) {
                    link = this.tableModel.buttons[i].link;
                    button.action = () => { TableActions.redirectToAction(link); };
                } else {
                    link = this.tableModel.buttons[i].link;
                    button.action = () => { TableActions.redirectToActionById(link, this.tableModel.name, null); }
                };
                buttons.push(button);
            }
        }
        return buttons;
    }

    private generateColumnDefsObject(): any {
        var columnDefs = [];
        try {
            this.classButtonsInRow = new Map<string, TableButtonInRow>();
            for (var i = 0; i < this.tableModel.buttonsInRow.length; i++) {
                var columnDef: any = {};
                columnDef.targets = this.tableModel.buttonsInRow[i].columnIndex;
                columnDef.defaultContent = "";
                columnDef.data = null;
                for (var j = 0; j < this.tableModel.buttonsInRow[i].buttons.length; j++) {
                    var btnInRow: TableButtonInRow = this.tableModel.buttonsInRow[i].buttons[j];
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
        } catch (e) {
            //ignore
        }
        return columnDefs;
    }

    private bindEvents() {
        this.classButtonsInRow.forEach((value, index, map) => {
            $("#" + this.tableModel.name + " tbody")
                .on('click',
                    '.' + index,
                    (ev) => {
                        if (!value.needId) {
                            TableActions.redirectToAction(value.link);
                        } else {
                            TableActions.redirectToActionById(value.link, this.tableModel.name, ev.target);
                        };
                    }
                );
        });
    }
}