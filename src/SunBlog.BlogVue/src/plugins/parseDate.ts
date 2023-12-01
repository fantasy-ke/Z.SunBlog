import moment from "moment";

export function parseDateTime(dateStr: string) {
  return moment(dateStr).format("YYYY-MM-DD HH:mm:ss");
}

export function parseDate(dateStr: string) {
  return moment(dateStr).format("YYYY年MM月DD日");
}

export function parseDateMonth(dateStr: string) {
  return moment(dateStr).format("MM月DD日");
}

export function parseTime(dateStr: string) {
  return moment(dateStr).format("MM-DD");
}

export function parseTimeH(dateStr: string) {
  return moment(dateStr).format("HH:mm");
}
