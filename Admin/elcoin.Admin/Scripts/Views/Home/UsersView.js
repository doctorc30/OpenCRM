var User = (function () {
    function User() {
    }
    return User;
}());
var UsersView = (function () {
    function UsersView(drawtreeCallBack) {
        this.users = [];
        this.drawtreeCallBack = drawtreeCallBack;
        UsersView.usersView = this;
        this.fillUsers();
        this.fillNewUsers();
        this.drawTree('network', 0, null);
        $("#dateFrom").datepicker({
            changeMonth: true,
            onClose: function (selectedDate) {
                $("#to").datepicker("option", "minDate", selectedDate);
            }
        });
        $("#dateTo").datepicker({
            changeMonth: true,
            onClose: function (selectedDate) {
                $("#from").datepicker("option", "maxDate", selectedDate);
            }
        });
        $('#modalUserInfo').on('hidden.bs.modal', function () {
            $("#childrenNetwork").css('height', '0');
            $("#childrenNetwork").text("");
        });
    }
    UsersView.prototype.left = function () {
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
    };
    UsersView.prototype.right = function () {
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
    };
    UsersView.prototype.setEditUser = function (user) {
        this.editUser = user;
        $("#leftfoot").prop("checked", false);
        $("#rightfoot").prop("checked", false);
        $("#modalTextSpread").text("Выберите сторону для пользователя " + this.editUser);
        $('#modalSpread').modal('show');
    };
    UsersView.prototype.getEditUser = function () {
        return this.editUser;
    };
    UsersView.prototype.showLine = function (button) {
        var line = $(button).attr("data");
        this.drawTree('network', line, null);
    };
    UsersView.prototype.clearFilter = function () {
        $("#dateFrom").val("");
        $("#dateTo").val("");
        $('#firstLineTable').DataTable().ajax.url(userLineJsonUsers + "?line=1");
        $('#firstLineTable').DataTable().ajax.reload();
        $('#modalSelectDate').modal('hide');
    };
    UsersView.prototype.filterUsersByDate = function () {
        var dateFrom = $("#dateFrom").val();
        var dateTo = $("#dateTo").val();
        $('#firstLineTable').DataTable().ajax.url(userLineJsonUsers + "?line=1&" + "dateFrom=" + dateFrom + "&dateTo=" + dateTo);
        $('#firstLineTable').DataTable().ajax.reload();
        $('#modalSelectDate').modal('hide');
    };
    UsersView.prototype.fillNewUsers = function () {
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
                    action: function () {
                        var userName = $('#newUsers').DataTable().rows({ selected: true }).data()[0][0];
                        UsersView.usersView.setEditUser(userName);
                    }
                }
            ]
        });
    };
    UsersView.prototype.fillUsers = function () {
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
                    action: function () {
                        var userName = $('#firstLineTable').DataTable().rows({ selected: true }).data()[0][0];
                        UsersView.usersView.showUserInfo(userName);
                    }
                },
                {
                    text: 'Фильтровать',
                    titleAttr: 'Фильтровать',
                    action: function () {
                        $('#modalSelectDate').modal('show');
                    }
                }
            ],
            fnRowCallback: function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
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
    };
    UsersView.prototype.showChildrenUsers = function () {
        $("#childrenNetwork").css('height', '500px');
        this.drawTree('childrenNetwork', 0, this.editUser);
    };
    UsersView.prototype.upgradRole = function () {
        $.ajax({
            type: "POST",
            data: {
                userName: this.editUser
            },
            url: upgradeRoelFirmUsers,
            success: function (json) {
                notify(json);
                $('#firstLineTable').DataTable().ajax.reload();
                $("#modalUserInfo").modal('hide');
            }
        });
    };
    UsersView.prototype.setWatchRole = function () {
        $.ajax({
            type: "POST",
            data: {
                userName: this.editUser
            },
            url: setWatchRoleUsers,
            success: function (json) {
                notify(json);
                $('#firstLineTable').DataTable().ajax.reload();
                $("#modalUserInfo").modal('hide');
            }
        });
    };
    UsersView.prototype.moveUser = function () {
        var moveToUserName = $("#targetName").val();
        $.ajax({
            type: "POST",
            data: {
                targetName: this.editUser,
                toName: moveToUserName
            },
            url: moveUserUsers,
            success: function (json) {
                notify(json);
                $('#firstLineTable').DataTable().ajax.reload();
                $("#modalUserInfo").modal('hide');
            }
        });
    };
    UsersView.prototype.showUserInfo = function (userName) {
        $.ajax({
            type: "POST",
            url: getUserInfoJsonUsers,
            data: {
                userName: userName
            },
            success: function (user) {
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
    };
    UsersView.prototype.getUsersFoot = function () {
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
    };
    UsersView.prototype.save = function () {
        var users = this.getUsersFoot();
        var data = {};
        data.users = users;
        $.ajax({
            type: "POST",
            data: data,
            url: setUsersFootUsers,
            accept: 'application/json',
            success: function (noty) {
                $('#modalSpread').modal('hide');
                UsersView.usersView.users = [];
                notify(noty);
                UsersView.usersView.drawTree('network', 0, null);
                $("#newUsers").DataTable().ajax.reload();
            }
        });
    };
    UsersView.prototype.drawTree = function (container, line, userName) {
        $.ajax({
            type: "POST",
            url: getTreeUsersJsonUsers,
            data: {
                line: line,
                userName: userName
            },
            success: function (json) {
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
    };
    return UsersView;
}());
//# sourceMappingURL=UsersView.js.map