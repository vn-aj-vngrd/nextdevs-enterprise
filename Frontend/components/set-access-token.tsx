"use client";

import { useSession } from "next-auth/react";
import { useEffect } from "react";
import { signOut } from "next-auth/react";

export default function SetAccessToken() {
  const { data: session, status } = useSession();

  useEffect(() => {
    const accessToken = session?.user.accessToken;
    if (accessToken) {
      localStorage.setItem("accessToken", accessToken);
    }

    if (status === "unauthenticated") {
      signOut();
    }
  }, [session, status]);

  return null;
}
