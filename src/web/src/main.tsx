import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import { BrowserRouter } from "react-router-dom";
import { Provider } from "react-redux";
import { store } from "./app/store.ts";
import { AxiosInterceptor } from "./app/AxiosInterceptor.ts";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <Provider store={store}>
      <BrowserRouter>
        <AxiosInterceptor />
        <App />
      </BrowserRouter>
    </Provider>
  </React.StrictMode>
);
