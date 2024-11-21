import { Metadata } from "next";
import LoginCard from "../_components/login-card";

import Header from "../_components/header";
import Footer from "../_components/footer";

export const metadata: Metadata = {
  title: "Sign In | NextDevs Inc."
};

export default function Page() {
  return (
    <div className="flex h-screen w-full flex-col items-center justify-between bg-background px-4 py-6">
      <Header />
      <LoginCard />
      <Footer />
    </div>
  );
}
