import KBar from "@/components/kbar";
import { AppSidebar } from "@/components/app-sidebar";
import Header from "@/components/layout/header";
import { SidebarInset, SidebarProvider } from "@/components/ui/sidebar";
import type { Metadata } from "next";
import { cookies } from "next/headers";
import SetAccessToken from "@/components/set-access-token";

export const metadata: Metadata = {
  title: "NextDevs Inc."
};

export default async function DashboardLayout({
  children
}: {
  children: React.ReactNode;
}) {
  const cookieStore = await cookies();
  const defaultOpen =
    cookieStore.get("sidebar:state")?.value === "true" || true;

  return (
    <KBar>
      <SetAccessToken />
      <SidebarProvider defaultOpen={defaultOpen}>
        <AppSidebar />
        <SidebarInset>
          <Header />
          {children}
        </SidebarInset>
      </SidebarProvider>
    </KBar>
  );
}
