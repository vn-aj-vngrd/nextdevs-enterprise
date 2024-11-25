import * as React from "react";
import { type SearchParams } from "@/types";

import { getValidFilters } from "@/lib/data-table";
import { Skeleton } from "@/components/ui/skeleton";
import { DataTableSkeleton } from "@/components/data-table/data-table-skeleton";
import { DateRangePicker } from "@/components/date-range-picker";

import { ProductTable } from "./_components/product-table";
import { searchParamsCache } from "./_lib/validations";
import {
  dehydrate,
  HydrationBoundary,
  QueryClient
} from "@tanstack/react-query";
import { client } from "@/lib/client";
import { Heading } from "@/components/ui/heading";
import { Separator } from "@/components/ui/separator";
import PageContainer from "@/components/layout/page-container";

interface IndexPageProps {
  searchParams: Promise<SearchParams>;
}

export default async function IndexPage(props: IndexPageProps) {
  const queryClient = new QueryClient();
  const searchParams = await props.searchParams;
  const { name, pageNumber, pageSize, sortCriteria, filters } =
    searchParamsCache.parse(searchParams);

  const validFilters = getValidFilters(filters);

  await queryClient.prefetchQuery({
    queryKey: ["products", name, pageNumber, pageSize, sortCriteria, filters],
    queryFn: () =>
      client.getPagedListProduct(
        name,
        pageNumber,
        pageSize,
        undefined,
        undefined
      )
  });

  return (
    <PageContainer>
      <Heading
        title={`Products`}
        description="Manage your products to keep track of your inventory."
      />

      <Separator />

      <React.Suspense fallback={<Skeleton className="h-7 w-52" />}>
        <DateRangePicker
          triggerSize="sm"
          triggerClassName="ml-auto w-56 sm:w-60"
          align="end"
          shallow={false}
        />
      </React.Suspense>

      <div className="min-w-0 flex-grow overflow-auto">
        <React.Suspense
          fallback={
            <DataTableSkeleton
              columnCount={6}
              searchableColumnCount={1}
              filterableColumnCount={2}
              cellWidths={["10rem", "40rem", "12rem", "12rem", "8rem", "8rem"]}
              shrinkZero
            />
          }
        >
          <HydrationBoundary state={dehydrate(queryClient)}>
            <ProductTable
              name={name}
              pageNumber={pageNumber}
              pageSize={pageSize}
              sortCriteria={sortCriteria}
              filters={validFilters}
            />
          </HydrationBoundary>
        </React.Suspense>
      </div>
    </PageContainer>
  );
}
