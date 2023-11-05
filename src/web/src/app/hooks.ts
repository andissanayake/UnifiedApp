import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";
import type { RootState } from "./store";
import { AnyAction, ThunkDispatch } from "@reduxjs/toolkit";
export type AppThunkDispatch = ThunkDispatch<RootState, any, AnyAction>;
// Use throughout your app instead of plain `useDispatch` and `useSelector`
export const useAppDispatch: () => AppThunkDispatch = useDispatch;
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;
