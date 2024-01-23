import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import { BrowserRouter } from "react-router-dom";
import { Provider } from "react-redux";
import { store } from "./app/store.ts";
import { AxiosApiInterceptor } from "./app/AxiosApiInterceptor.ts";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <Provider store={store}>
      <BrowserRouter>
        <AxiosApiInterceptor />
        <App />
      </BrowserRouter>
    </Provider>
  </React.StrictMode>
);
