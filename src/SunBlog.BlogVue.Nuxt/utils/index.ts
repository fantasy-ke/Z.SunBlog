import type { Dayjs } from "dayjs";
import moment from "moment";

/**
 * 随机整数
 * @param min 最小值
 * @param max 最大值
 * @returns
 */
export const randomNumber = (min: number, max: number) => {
    return Math.floor(Math.random() * (max - min + 1)) + min;
  };
  
  /**
   * 生成uuid
   * @returns 
   */
export const generateUUID = () => {
    return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, function (c) {
      const r = (Math.random() * 16) | 0, 
        v = c === "x" ? r : (r & 0x3) | 0x8;
      return v.toString(16);
    });
  };
  
export const formatDate = (date: string | number | moment.Moment | Date  | null | undefined, format: string = "YYYY-MM-DD HH:mm:ss"): string => {
    return moment(date).format(format)
};