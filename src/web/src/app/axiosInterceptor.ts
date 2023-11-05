import { useEffect } from "react";
import axios from "axios";
import {
  resetToken,
  selectAuth,
  updateToken,
} from "../features/user/authSlice";
import { useAppDispatch, useAppSelector } from "./hooks";
import { refreshToken } from "../features/user/authAPI";

export const AxiosInterceptor = () => {
  const authData = useAppSelector(selectAuth);
  const dispatch = useAppDispatch();
  useEffect(() => {
    const requestInterceptor = axios.interceptors.request.use(
      async (config) => {
        const accessToken = authData.accessToken;
        if (accessToken && !config.headers.Authorization) {
          config.headers.Authorization = `Bearer ${accessToken}`;
        }
        return config;
      }
    );

    // Response interceptor
    const responseInterceptor = axios.interceptors.response.use(
      (response) => response,
      async (error) => {
        if (error.response && error.response.status === 401) {
          // Token expired, attempt to refresh it
          if (authData.refreshToken && authData.accessToken) {
            // Make a refresh token request and update access token
            try {
              const response = await refreshToken({
                accessToken: authData.accessToken,
                refreshToken: authData.refreshToken,
              });
              if (response.isSucceed && response.data) {
                dispatch(updateToken(response.data));
                error.config.headers.Authorization = `Bearer ${response.data.accessToken}`;
                return axios.request(error.config);
              } else {
                dispatch(resetToken());
              }
            } catch (refreshError) {
              dispatch(resetToken());
              throw refreshError;
            }
          } else {
            dispatch(resetToken());
          }
        }

        return Promise.reject(error);
      }
    );

    return () => {
      // Cleanup: Remove the interceptors when the component unmounts
      axios.interceptors.request.eject(requestInterceptor);
      axios.interceptors.response.eject(responseInterceptor);
    };
  }, [authData, dispatch]);

  return null; // This component doesn't render anything
};
