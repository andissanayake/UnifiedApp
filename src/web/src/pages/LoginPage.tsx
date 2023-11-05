import { Breadcrumb } from "antd";
import { Login } from "../features/user/Login";

export const LoginPage = () => (
  <>
    <Breadcrumb
      style={{ margin: "16px 0" }}
      items={[{ key: 1, title: "Login", separator: "/" }]}
    ></Breadcrumb>
    <Login></Login>
  </>
);
