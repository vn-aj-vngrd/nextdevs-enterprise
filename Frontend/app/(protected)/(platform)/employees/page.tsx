import { searchParamsCache } from "@/lib/search-params";
import { type SearchParams } from "nuqs/server";
import React from "react";
import EmployeeListingPage from "./_components/employee-listing-page";
import { Metadata } from "next";

type PageProps = {
  searchParams: Promise<SearchParams>;
};

export const metadat: Metadata = {
  title: "Employees | NextDevs Inc."
};

export default async function Page({ searchParams }: PageProps) {
  // Allow nested RSCs to access the search params (in a type-safe way)
  await searchParamsCache.parse(searchParams);

  return <EmployeeListingPage />;
}
