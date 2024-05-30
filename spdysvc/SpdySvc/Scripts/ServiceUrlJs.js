function ServiceUrl(Controller, Action) {
    this.Cntrlr = Controller;
    this.Actn = Action;
    this.Url = "../" + this.Cntrlr + "/" + this.Actn;
    this.basepath = window.location.pathname;
    this.GenerateUrl = function () {
        var splt = this.basepath.split('/');
        return this.Url;

    }
}