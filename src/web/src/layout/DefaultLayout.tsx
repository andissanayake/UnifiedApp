import { App, Layout, Menu } from "antd";
import { Content, Header } from "antd/es/layout/layout";
import { useEffect, useState } from "react";
import { Outlet, useLocation, useNavigate } from "react-router";
import { AppLogo } from "../features/AppLogo";
import { AppFooter } from "../features/Footer";

export const DefaultLayout = () => {
  const navigate = useNavigate();
  let location = useLocation();
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
            items={[
              {
                key: "/",
                label: "Home",
                onClick: (e) => {
                  handleClick(e.key);
                },
              },
              {
                key: "/login",
                label: "Login",
                onClick: (e) => {
                  handleClick(e.key);
                },
              },
              {
                key: "/register",
                label: "Register",
                onClick: (e) => {
                  handleClick(e.key);
                },
              },
            ]}
          />
        </Header>
        <Content style={{ padding: "0 50px", minHeight: "400px" }}>
          <Outlet />
        </Content>
      </Layout>
      <AppFooter />
    </App>
  );
};
