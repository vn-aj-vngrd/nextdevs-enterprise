import { NextAuthConfig } from "next-auth";
import CredentialProvider from "next-auth/providers/credentials";
import { authenticate } from "./server/account";

const authConfig = {
  providers: [
    CredentialProvider({
      credentials: {
        username: {
          type: "text"
        },
        password: {
          type: "password"
        }
      },
      async authorize(credentials) {
        const uri = `${process.env.NEXT_PUBLIC_API_URL}/account/authenticate`;

        const response = await fetch(uri, {
          method: "POST",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(credentials)
        });

        const result = await response.json();
        const data = result.data;

        if (data) {
          return {
            id: data.id,
            name: data.userName,
            email: data.email
          };
        } else {
          throw new Error(result.errors);
        }
      }
    })
  ],
  callbacks: {
    authorized({ auth, request: { nextUrl } }) {
      const isLoggedIn = !!auth?.user;
      const isOnDashboard = nextUrl.pathname.startsWith("/dashboard");
      if (isOnDashboard) {
        if (isLoggedIn) return true;
        return false; // Redirect unauthenticated users to login page
      } else if (isLoggedIn) {
        return Response.redirect(new URL("/dashboard", nextUrl));
      }
      return true;
    }
  },

  pages: {
    signIn: "/"
  }
} satisfies NextAuthConfig;

export default authConfig;
