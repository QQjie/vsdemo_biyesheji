function StringBuilder() {
    this._asBuilder = [];
}
StringBuilder.prototype.clear=function(){
    this._asBuilder = [];

}
StringBuilder.prototype.append = function () {
    Array.prototype.push.apply(this._asBuilder, arguments);
    return this;//实现连续追加
}
StringBuilder.prototype.toString = function () {
    return this._asBuilder.join("");
}