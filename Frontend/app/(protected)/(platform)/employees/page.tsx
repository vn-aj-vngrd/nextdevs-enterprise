import { searchParamsCache } from "@/lib/search-params";
import { type SearchParams } from "nuqs/server";
import React from "react";
import EmployeeListingPage from "./_components/employee-listing-page";
import { Metadata } from "next";

type PageProps = {
  searchParams: Promise<SearchParams>;
};

export const metadata: Metadata = {
  title: "Employees | NextDevs Inc."
};

export default async function Page({ searchParams }: PageProps) {
  await searchParamsCache.parse(searchParams);

  return <EmployeeListingPage />;
}
