import { App, Button, Form, FormInstance, Input } from "antd";
import React from "react";
import { register } from "./authAPI";
import { useNavigate } from "react-router";
import { resetLoading, setLoading } from "./authSlice";
import { useAppDispatch } from "../../app/hooks";

type FieldType = {
  email?: string;
  password?: string;
};

export const Register = () => {
  const formRef = React.useRef<FormInstance>(null);
  const { message } = App.useApp();
  const navigate = useNavigate();
  const dispatch = useAppDispatch();

  const onFinish = async (values: FieldType) => {
    dispatch(setLoading());

    const data = await register(
      values.email as string,
      values.password as string
    );
    dispatch(resetLoading());
    if (data.isSucceed) {
      message.success("Registration is successful, Please login.");
      navigate("/login");
    } else {
      data.messages?.DuplicateUserName &&
        formRef.current?.setFields([
          { name: "email", errors: data.messages?.DuplicateUserName },
        ]);
      data.messages?.password &&
        formRef.current?.setFields([
          { name: "password", errors: data.messages?.password },
        ]);
    }
  };
  return (
    <>
      <Form
        name="basic"
        labelCol={{ span: 8 }}
        wrapperCol={{ span: 16 }}
        style={{ maxWidth: 600 }}
        initialValues={{ remember: true }}
        onFinish={onFinish}
        autoComplete="off"
        ref={formRef}
      >
        <Form.Item<FieldType>
          label="Email"
          name="email"
          rules={[
            {
              required: true,
              message: "Please input your Email!",
            },
            {
              type: "email",
              message: "Please enter valid Email!",
            },
          ]}
        >
          <Input />
        </Form.Item>

        <Form.Item<FieldType>
          label="Password"
          name="password"
          extra={
            <ul>
              <li>
                Minimum Length: The password must be at least 6 characters long.
              </li>
              <li>
                At least 1 Uppercase Letter: Include at least one uppercase
                letter (A-Z).
              </li>
              <li>
                At least 1 Lowercase Letter: Include at least one lowercase
                letter (a-z).
              </li>
              <li>
                At least 1 Special Character: Include at least one special
                character (e.g., !@#$%^&*).
              </li>
            </ul>
          }
          rules={[
            { required: true, message: "Please input your password!" },
            {
              pattern: new RegExp(
                "^(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*]).{6,}$"
              ),
              message: "Please check password requirements",
            },
          ]}
        >
          <Input.Password />
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
