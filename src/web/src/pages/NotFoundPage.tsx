import { Breadcrumb } from "antd";

export const NotFoundPage = () => {
  return (
    <>
      <Breadcrumb
        style={{ margin: "16px 0" }}
        items={[{ key: 1, title: "Not Found", separator: "/" }]}
      ></Breadcrumb>
    </>
  );
};
