// dateiso extra validator
// Guillaume Potier
window.ParsleyConfig = window.ParsleyConfig || {};
window.ParsleyConfig.validators = window.ParsleyConfig.validators || {};

window.ParsleyConfig.validators.dateiso = {
  fn: function (value) {
      return /^([12]\d|0[1-9]|3[01])\D?(0[1-9]|1[0-2])\D?(\d{4})$/.test(value);
  },
  priority: 256
};
window.ParsleyConfig.validators.daterangeiso = {
    fn: function (value) {
        return /^([12]\d|0[1-9]|3[01])\D?(0[1-9]|1[0-2])\D?(\d{4}) - ([12]\d|0[1-9]|3[01])\D?(0[1-9]|1[0-2])\D?(\d{4})$/.test(value);
    },
    priority: 256
};