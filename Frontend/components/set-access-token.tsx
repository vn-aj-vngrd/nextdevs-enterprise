"use client";

import { useSession } from "next-auth/react";
import { useEffect } from "react";

export default function SetAccessToken() {
  const { data: session } = useSession();

  useEffect(() => {
    if (localStorage.getItem("accessToken")) {
      return;
    }

    const accessToken = session?.user.accessToken;
    if (accessToken) {
      localStorage.setItem("accessToken", accessToken);
    }
  }, [session]);

  return null;
}
