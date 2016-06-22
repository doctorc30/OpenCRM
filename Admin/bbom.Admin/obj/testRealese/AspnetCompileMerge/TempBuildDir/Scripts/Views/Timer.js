var Timer = (function () {
    function Timer(hourPlace, minPlace, secPlace) {
        this.secPlace = secPlace;
        this.minPlace = minPlace;
        this.hourPlace = hourPlace;
    }
    Timer.prototype.getTimer = function (string) {
        var dateNew = string;
        var dateT = new Date(dateNew);
        var date = new Date();
        if (dateT > date) {
            var day = String(dateT.getDay() - date.getDay());
            if (parseInt(day) < 10) {
                day = "0" + day;
            }
            day = day.toString();
            var hour = this.getMax(dateT.getHours(), date.getHours(), 24);
            if (parseInt(hour) < 10) {
                hour = "0" + hour;
            }
            hour = hour.toString();
            var min = this.getMax(dateT.getMinutes(), date.getMinutes(), 60);
            if (parseInt(min) < 10) {
                min = "0" + min;
            }
            min = min.toString();
            var sec = this.getMax(dateT.getSeconds(), date.getSeconds(), 60);
            if (parseInt(sec) < 10) {
                sec = "0" + sec;
            }
            sec = sec.toString();
            this.dayTime = day + " : " + hour + " : " + min + " : " + sec;
            this.hourTime = hour + " : " + min + " : " + sec;
            this.hourPlace.text(hour);
            this.minPlace.text(min);
            this.secPlace.text(sec);
        }
        else {
            this.hourPlace.text("00");
            this.minPlace.text("00");
            this.secPlace.text("00");
        }
    };
    Timer.prototype.start = function (date) {
        var _this = this;
        if (this.handle != null)
            clearInterval(this.handle);
        this.getTimer(date);
        this.handle = setInterval(function () { _this.getTimer(date); }, 1000);
    };
    Timer.prototype.getMax = function (a, b, n) {
        if (n === 24)
            n--;
        if (a > b)
            return a - b;
        if (a === b)
            return n;
        else
            return n - (b - a);
    };
    return Timer;
}());
//# sourceMappingURL=Timer.js.map