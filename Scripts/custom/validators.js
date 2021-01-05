//$.validator.methods.date = function (value, element) {
//    return this.optional(element) || parseDate(value, "yyyy-MM-dd") !== null;
//}

$.validator.methods.date = function (value, element) {
    return true;
    //return this.optional(element) || $.datepicker.parseDate('dd/MM/yyyy', value);
};