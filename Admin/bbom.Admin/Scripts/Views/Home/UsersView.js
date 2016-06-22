class User {
}
class UsersView {
    constructor(drawtreeCallBack) {
        this.users = [];
        this.drawtreeCallBack = drawtreeCallBack;
        UsersView.usersView = this;
        this.fillUsers();
        this.fillNewUsers();
        this.drawTree('network', 0, null);
        $("#dateFrom").datepicker({
            changeMonth: true,
            onClose(selectedDate) {
                $("#to").datepicker("option", "minDate", selectedDate);
            }
        });
        $("#dateTo").datepicker({
            changeMonth: true,
            onClose(selectedDate) {
                $("#from").datepicker("option", "maxDate", selectedDate);
            }
        });
        $('#modalUserInfo').on('hidden.bs.modal', () => {
            $("#childrenNetwork").css('height', '0');
            $("#childrenNetwork").text("");
        });
    }
    left() {
        var user = {
            userName: this.editUser,
            foot: 0
        };
        for (var i = 0; i < this.users.length; i++) {
            if (this.users[i].userName === user.userName) {
                this.users[i] = user;
                return;
            }
        }
        this.users.push(user);
    }
    right() {
        var user = {
            userName: this.editUser,
            foot: 1
        };
        for (var i = 0; i < this.users.length; i++) {
            if (this.users[i].userName === user.userName) {
                this.users[i] = user;
                return;
            }
        }
        this.users.push(user);
    }
    setEditUser(user) {
        this.editUser = user;
        $("#leftfoot").prop("checked", false);
        $("#rightfoot").prop("checked", false);
        $("#modalTextSpread").text("Выберите сторону для пользователя " + this.editUser);
        $('#modalSpread').modal('show');
    }
    getEditUser() {
        return this.editUser;
    }
    showLine(button) {
        var line = $(button).attr("data");
        this.drawTree('network', line, null);
    }
    clearFilter() {
        $("#dateFrom").val("");
        $("#dateTo").val("");
        $('#firstLineTable').DataTable().ajax.url(userLineJsonUsers + "?line=1");
        $('#firstLineTable').DataTable().ajax.reload();
        $('#modalSelectDate').modal('hide');
    }
    filterUsersByDate() {
        var dateFrom = $("#dateFrom").val();
        var dateTo = $("#dateTo").val();
        $('#firstLineTable').DataTable().ajax.url(userLineJsonUsers + "?line=1&" + "dateFrom=" + dateFrom + "&dateTo=" + dateTo);
        $('#firstLineTable').DataTable().ajax.reload();
        $('#modalSelectDate').modal('hide');
    }
    fillNewUsers() {
        $('#newUsers').DataTable({
            pageLength: 10,
            ajax: {
                url: newUsersJsonUsers + "?line=1",
                dataSrc: ''
            },
            dom: 'Bfrtip',
            select: true,
            buttons: [
                {
                    text: 'Расспределить',
                    action() {
                        var userName = $('#newUsers').DataTable().rows({ selected: true }).data()[0][0];
                        UsersView.usersView.setEditUser(userName);
                    }
                }
            ]
        });
    }
    fillUsers() {
        var table = $('#firstLineTable').DataTable({
            pageLength: 10,
            ajax: {
                url: userLineJsonUsers + "?line=1",
                dataSrc: ''
            },
            dom: 'Bfrtip',
            select: true,
            buttons: [
                {
                    text: 'Информация',
                    titleAttr: 'Информация',
                    action() {
                        var userName = $('#firstLineTable').DataTable().rows({ selected: true }).data()[0][0];
                        UsersView.usersView.showUserInfo(userName);
                    }
                },
                {
                    text: 'Фильтровать',
                    titleAttr: 'Фильтровать',
                    action() {
                        $('#modalSelectDate').modal('show');
                    }
                }
            ],
            fnRowCallback(nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                for (var i = 0; i < aData.length; i++) {
                    switch (aData[i]) {
                        case 'нет':
                            $(nRow.cells[i]).addClass('red-cell');
                            break;
                        case 'да':
                            $(nRow.cells[i]).addClass('green-cell');
                            break;
                    }
                }
            }
        });
        $('#firstLineTable tbody').on('click', 'tr', function () {
            // ReSharper disable SuspiciousThisUsage
            $(this).dblclick(function () {
                var userName = table.row(this).data();
                UsersView.usersView.showUserInfo(userName);
            });
            // ReSharper restore SuspiciousThisUsage
        });
    }
    showChildrenUsers() {
        $("#childrenNetwork").css('height', '500px');
        this.drawTree('childrenNetwork', 0, this.editUser);
    }
    upgradRole() {
        $.ajax({
            type: "POST",
            data: {
                userName: this.editUser
            },
            url: upgradeRoelFirmUsers,
            success(json) {
                notify(json);
                $('#firstLineTable').DataTable().ajax.reload();
                $("#modalUserInfo").modal('hide');
            }
        });
    }
    setWatchRole() {
        $.ajax({
            type: "POST",
            data: {
                userName: this.editUser
            },
            url: setWatchRoleUsers,
            success(json) {
                notify(json);
                $('#firstLineTable').DataTable().ajax.reload();
                $("#modalUserInfo").modal('hide');
            }
        });
    }
    moveUser() {
        var moveToUserName = $("#targetName").val();
        $.ajax({
            type: "POST",
            data: {
                targetName: this.editUser,
                toName: moveToUserName
            },
            url: moveUserUsers,
            success(json) {
                notify(json);
                $('#firstLineTable').DataTable().ajax.reload();
                $("#modalUserInfo").modal('hide');
            }
        });
    }
    showUserInfo(userName) {
        $.ajax({
            type: "POST",
            url: getUserInfoJsonUsers,
            data: {
                userName: userName
            },
            success(user) {
                UsersView.usersView.editUser = user.userName;
                $('#dialogUserName').text(user.fio + "(" + user.userName + ")");
                $('#dialogUserEmail').text(user.email);
                $('#dialogUserInvited').text(user.parentUserName);
                $('#confimInvite').text(user.confimInvite);
                $('#lastLogin').text(user.lastLogin);
                $('#isWatch').text(user.isWatch);
                $('#isUser').text(user.isUser);
                var comms = $('#comms');
                comms.empty();
                for (var i = 0; i < user.communications.length; i++) {
                    var com = user.communications[i];
                    comms.append('<h4>' + com.name + '</h4><label>' + com.value + '</label>');
                }
                if (userIsRole("admin")) {
                    $("#moveUserPlace").css("display", "inherit");
                }
                if (user.itCanBeUpgraded === "True" && (userIsRole("admin") || userIsRole("payFirm"))) {
                    $('#upgradRole').css("display", "inherit");
                }
                else {
                    $('#upgradRole').css("display", "none");
                }
                if (user.itNotWatched === "True") {
                    $('#setWatchRole').css("display", "inherit");
                }
                else {
                    $('#setWatchRole').css("display", "none");
                }
                $('#modalUserInfo').modal('show');
            }
        });
    }
    getUsersFoot() {
        //var childrens = $("#users").find("input");
        //for (var i = 0; i < childrens.length; i++) {
        //    var userName = $(childrens[i]).attr('name');
        //    var foot = $(childrens[i]).val();
        //    var user = {
        //        "userName": userName,
        //        "foot": foot
        //    };
        //    users.push(user);
        //}
        return this.users;
    }
    save() {
        var users = this.getUsersFoot();
        var data = {};
        data.users = users;
        $.ajax({
            type: "POST",
            data: data,
            url: setUsersFootUsers,
            accept: 'application/json',
            success(noty) {
                $('#modalSpread').modal('hide');
                UsersView.usersView.users = [];
                notify(noty);
                UsersView.usersView.drawTree('network', 0, null);
                $("#newUsers").DataTable().ajax.reload();
            }
        });
    }
    drawTree(container, line, userName) {
        $.ajax({
            type: "POST",
            url: getTreeUsersJsonUsers,
            data: {
                line: line,
                userName: userName
            },
            success(json) {
                var nodes = json.nodes;
                var edges = json.edges;
                if (edges.length === 0) {
                    $("#" + container).text("У вас пока нету дочерних распределенных пользователей");
                    return;
                }
                var data = {
                    nodes: nodes,
                    edges: edges
                };
                var options = {
                    //groups: {
                    //    users: {
                    //        shape: 'icon',
                    //        icon: {
                    //            face: 'FontAwesome',
                    //            code: '\uf007',
                    //            size: 30,
                    //            color: '#aa00ff'
                    //        }
                    //    }
                    //},
                    layout: {
                        hierarchical: {
                            direction: "UD",
                            sortMethod: 'directed'
                        }
                    },
                    nodes: {
                        shape: 'dot',
                        size: 10
                    },
                    edges: {
                        smooth: true,
                        arrows: { to: true }
                    }
                };
                if (UsersView.usersView.drawtreeCallBack != null)
                    UsersView.usersView.drawtreeCallBack(container, data, options);
            }
        });
    }
}
//# sourceMappingURL=UsersView.js.map