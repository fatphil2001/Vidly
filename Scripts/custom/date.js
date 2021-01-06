$(function () {
    $('input[type=datetime]').datepicker({
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        yearRange: "-130:+0",
        onChangeMonthYear: function (year, month, inst) {
            var format = inst.settings.dateFormat
            var selectedDate = new Date(inst.selectedYear, inst.selectedMonth, inst.selectedDay)
            var date = $.datepicker.formatDate(format, selectedDate);
            $(this).datepicker("setDate", date);
        }
    });
});

$.validator.methods.date = function (value, element) {
    //return true;
    return this.optional(element) || $.datepicker.parseDate('dd/mm/yyyy', value);
};