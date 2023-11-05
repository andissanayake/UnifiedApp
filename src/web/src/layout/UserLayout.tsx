import { App, Layout, Menu, Spin } from "antd";
import { Content, Header } from "antd/es/layout/layout";
import { useEffect, useState } from "react";
import { Outlet, useLocation, useNavigate } from "react-router";
import { iUser, logoutAsync } from "../features/user/authSlice";
import { useAppDispatch } from "../app/hooks";
import { AppLogo } from "../features/AppLogo";
import { AppFooter } from "../features/Footer";

export const UserLayout = ({ UserName }: iUser) => {
  const navigate = useNavigate();
  const location = useLocation();
  const dispatch = useAppDispatch();
  const [current, setCurrent] = useState(
    location.pathname === "/" || location.pathname === ""
      ? "/"
      : location.pathname
  );

  useEffect(() => {
    if (location) {
      if (current !== location.pathname) {
        setCurrent(location.pathname);
      }
    }
  }, [location]);

  const handleClick = (key: string) => {
    navigate(key);
  };
  return (
    <App>
      <Layout className="layout">
        <Header style={{ display: "flex", alignItems: "center" }}>
          <AppLogo />
          <Menu
            theme="dark"
            mode="horizontal"
            defaultSelectedKeys={["/"]}
            selectedKeys={[current]}
            style={{ minWidth: "500px" }}
            items={[
              {
                key: "/",
                label: "Home",
                onClick: (e) => {
                  handleClick(e.key);
                },
              },
              {
                key: "/user",
                label: UserName,
              },
              {
                key: "/logout",
                label: "Logout",
                onClick: () => {
                  dispatch(logoutAsync());
                  handleClick("/");
                },
              },
            ]}
          />
        </Header>
        <Content style={{ padding: "0 50px", minHeight: "400px" }}>
          <Outlet />
        </Content>
        <AppFooter />
      </Layout>
    </App>
  );
};
