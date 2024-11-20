import { NextAuthConfig } from "next-auth";
import CredentialProvider from "next-auth/providers/credentials";
import { authenticate } from "./server/account";

const authConfig = {
  providers: [
    CredentialProvider({
      credentials: {
        userName: {
          type: "text"
        },
        password: {
          type: "password"
        }
      },
      async authorize(credentials, req) {
        const res = authenticate({
          userName: credentials.userName as string,
          password: credentials.password as string
        });

        // If no error is thrown, the user is authenticated

        // If the user is authenticated, return an object with the user info
        return {
          id: "1",
          name: "John Doe",
          email: ""
        };
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
