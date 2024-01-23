interface iMessages {
  [key: string]: string[];
}
export interface iAppResponse<T> {
  data?: T;
  messages: iMessages;
  isSucceed: boolean;
}
