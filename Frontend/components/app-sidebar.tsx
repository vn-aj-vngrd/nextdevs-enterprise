"use client";

import * as React from "react";
import {
  Barcode,
  BringToFront,
  FolderKanban,
  LayoutDashboard,
  Network,
  Sparkles,
  Users
} from "lucide-react";

import { NavMain } from "@/components/nav-main";
import { NavTools } from "@/components/nav-projects";
import { NavUser } from "@/components/nav-user";
import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarHeader,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem
} from "@/components/ui/sidebar";
import { useSession } from "next-auth/react";
import Image from "next/image";
import { useTheme } from "next-themes";
import { Separator } from "./ui/separator";

const data = {
  navMain: [
    {
      title: "Dashboard",
      url: "/dashboard",
      icon: LayoutDashboard,
      isActive: true,
      items: []
    },
    {
      title: "Products",
      url: "/products",
      icon: Barcode,
      items: []
    },
    {
      title: "Orders",
      url: "/orders",
      icon: BringToFront,
      items: []
    },
    {
      title: "Employees",
      url: "/employees",
      icon: Network,
      items: []
    },
    {
      title: "Users",
      url: "/users",
      icon: Users,
      items: []
    }
  ],
  navTools: [
    {
      name: "Kanban",
      url: "/kanban",
      icon: FolderKanban
    },
    {
      name: "AI",
      url: "/ai",
      icon: Sparkles
    }
  ]
};

export function AppSidebar({ ...props }: React.ComponentProps<typeof Sidebar>) {
  const { data: session } = useSession();
  const { resolvedTheme } = useTheme();

  const src =
    resolvedTheme === "light"
      ? "/assets/branding/SVG/next_devs_logo.svg"
      : "/assets/branding/SVG/next_devs_logo_dark.svg";

  return (
    <Sidebar variant="inset" {...props} collapsible="icon">
      <SidebarHeader>
        <SidebarMenu>
          <SidebarMenuItem>
            <SidebarMenuButton size="lg" asChild>
              <a href="">
                <Image
                  className="flex aspect-square size-8 items-center justify-center rounded-lg bg-primary p-1.5 text-sidebar-primary-foreground"
                  src={src}
                  width={32}
                  height={32}
                  alt="N"
                ></Image>
                <div className="grid flex-1 text-left text-sm leading-tight">
                  <span className="truncate font-semibold">NextDevs Inc.</span>
                  <span className="truncate text-xs">Enterprise</span>
                </div>
              </a>
            </SidebarMenuButton>
          </SidebarMenuItem>
        </SidebarMenu>
      </SidebarHeader>
      <SidebarContent>
        <NavMain items={data.navMain} />
        <NavTools tools={data.navTools} />
      </SidebarContent>

      <Separator />
      <SidebarFooter>
        <NavUser
          user={{
            name: session?.user?.name ?? "User",
            email: session?.user?.email ?? "",
            avatar: session?.user?.image ?? ""
          }}
        />
      </SidebarFooter>
    </Sidebar>
  );
}
