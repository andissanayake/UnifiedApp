import "./App.css";
import "bootstrap/dist/css/bootstrap-reboot.min.css";
import "bootstrap/dist/css/bootstrap-utilities.min.css";
import { Route, Routes } from "react-router";
import { DefaultLayout } from "./layout/DefaultLayout";
import { HomePage } from "./pages/HomePage";
import { RegisterPage } from "./pages/RegisterPage";
import { LoginPage } from "./pages/LoginPage";
import { NotFoundPage } from "./pages/NotFoundPage";
import { useAppSelector } from "./app/hooks";
import { selectAuth } from "./features/user/authSlice";
import { UserLayout } from "./layout/UserLayout";
import { Spin } from "antd";
export const App = () => {
  const auth = useAppSelector(selectAuth);
  if (!auth.user) {
    return (
      <Spin spinning={auth.status == "loading"}>
        <Routes>
          <Route path="/" element={<DefaultLayout />}>
            <Route index element={<HomePage />} />
            <Route path="register" element={<RegisterPage />} />
            <Route path="login" element={<LoginPage />} />
            <Route path="*" element={<NotFoundPage />} />
          </Route>
        </Routes>
      </Spin>
    );
  } else {
    return (
      <>
        <Spin spinning={auth.status == "loading"}>
          <Routes>
            <Route path="/" element={<UserLayout {...auth.user} />}>
              <Route index element={<HomePage />} />
              <Route path="*" element={<NotFoundPage />} />
            </Route>
          </Routes>
        </Spin>
      </>
    );
  }
};

export default App;
