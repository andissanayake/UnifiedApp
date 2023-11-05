import { Breadcrumb, Button, message } from "antd";
import { profileApi } from "../features/user/authAPI";
import { useState } from "react";
import { useAppSelector } from "../app/hooks";
import { selectAuth } from "../features/user/authSlice";

export const HomePage = () => {
  const auth = useAppSelector(selectAuth);
  const [userName, setUserName] = useState(
    `Please click "Get profile name button" to test profile api`
  );
  const click = () => {
    profileApi()
      .then((d) => setUserName(d))
      .catch((e) => {
        console.log(e);
        message.info("Please login to access this api");
        setUserName("401 - unauthorized");
      });
  };
  return (
    <>
      <Breadcrumb
        style={{ margin: "16px 0" }}
        items={[{ key: 1, title: "Home", separator: "/" }]}
      ></Breadcrumb>
      <h2>User object from store</h2>
      <pre>{JSON.stringify(auth, null, 2)}</pre>
      <p>
        Profile api responce -{">"} <b>{userName}</b>
      </p>
      <Button onClick={click}>Get profile name</Button>
    </>
  );
};
