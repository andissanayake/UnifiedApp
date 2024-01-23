import axios from "axios";
import { iAppResponse } from "../../app/appResponse";

const BASE_URL = "https://localhost:1002";

export const login = async (email: string, password: string) => {
  const response = await axios.post<
  iAppResponse<{ accessToken: string; refreshToken: string }>
  >(`${BASE_URL}/user/login`, {
    email: email,
    password: password,
  }).catch((ex)=>{
    console.log(ex);
  });
  return response?.data;
};
export const refreshToken = async (data: {
  accessToken: string;
  refreshToken: string;
}) => {
  const response = await axios.post<
  iAppResponse<{ accessToken: string; refreshToken: string }>
  >(`${BASE_URL}/user/refreshToken`, data);
  return response.data;
};
export const register = async (email: string, password: string) => {
  const response = await axios.post<iAppResponse<{}>>(
    `${BASE_URL}/user/register`,
    {
      email: email,
      password: password,
    }
  );
  return response.data;
};
export const logout = async () => {
  const response = await axios.post<iAppResponse<boolean>>(
    `${BASE_URL}/user/logout`
  );
  return response.data;
};
export const profileApi = async () => {
  const response = await axios.post(`${BASE_URL}/user/profile`);
  return response.data;
};
