import { App, Button, Form, FormInstance, Input } from "antd";
import React from "react";
import { login } from "./authAPI";
import { useAppDispatch } from "../../app/hooks";
import { updateToken, resetLoading, setLoading } from "./authSlice";
import { useNavigate } from "react-router";
type FieldType = {
  email?: string;
  password?: string;
};

export const Login = () => {
  const navigate = useNavigate();

  const dispatch = useAppDispatch();
  const { message } = App.useApp();
  const formRef = React.useRef<FormInstance>(null);
  const onFinish = async (values: FieldType) => {
    dispatch(setLoading());
    const data = await login(values.email as string, values.password as string);
    dispatch(resetLoading());
    if (data?.isSucceed && data?.data) {
      message.success("Login is successful.");
      dispatch(updateToken(data.data));
      navigate("/");
    } else if(data!=null) {
      data?.messages?.email &&
        formRef.current?.setFields([
          { name: "email", errors: data.messages?.email },
        ]);
      data?.messages?.password &&
        formRef.current?.setFields([
          { name: "password", errors: data.messages?.password },
        ]);
    }else{
      message.error("Unexpected error occurred please try again later.");
    }
  };
  return (
    <>
      <Form
        name="unified-app-login"
        id="unified-app-login"
        labelCol={{ span: 8 }}
        wrapperCol={{ span: 16 }}
        style={{ maxWidth: 600 }}
        initialValues={{ remember: true }}
        onFinish={onFinish}
        autoComplete="unified-app-login"
        ref={formRef}
      >
        <Form.Item<FieldType>
          label="email"
          name="email"
          rules={[{ required: true, message: "Please input your Email!" }]}
        >
          <Input
            name="unified-app-login-name"
            id="unified-app-login-name"
            autoComplete="unified-app-login-name"
          />
        </Form.Item>

        <Form.Item<FieldType>
          label="Password"
          name="password"
          rules={[{ required: true, message: "Please input your Password!" }]}
        >
          <Input.Password
            name="unified-app-login-password"
            id="unified-app-login-password"
            autoComplete="unified-app-login-password"
          />
        </Form.Item>

        <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
          <Button type="primary" htmlType="submit">
            Submit
          </Button>
        </Form.Item>
      </Form>
    </>
  );
};
