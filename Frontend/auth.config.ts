import { NextAuthConfig } from "next-auth";
import CredentialProvider from "next-auth/providers/credentials";

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
      async authorize(credentials, req) {
        setTimeout(() => {}, 5000);
        const user = {
          id: "1",
          name: "John",
          email: "test"
        };
        if (user) {
          return user;
        } else {
          return null;
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
