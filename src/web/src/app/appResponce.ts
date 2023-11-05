interface iMessages {
  [key: string]: string[];
}
export interface iAppResponce<T> {
  data?: T;
  messages: iMessages;
  isSucceed: boolean;
}
