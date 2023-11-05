import { Breadcrumb } from "antd";
import { Register } from "../features/user/Register";

export const RegisterPage = () => (
  <>
    <Breadcrumb
      style={{ margin: "16px 0" }}
      items={[{ key: 1, title: "Register", separator: "/" }]}
    ></Breadcrumb>
    <Register />
  </>
);
