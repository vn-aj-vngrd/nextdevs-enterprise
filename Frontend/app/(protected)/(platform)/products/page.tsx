import * as React from "react";
import { type SearchParams } from "@/types";

import { getValidFilters } from "@/lib/data-table";
import { Skeleton } from "@/components/ui/skeleton";
import { DataTableSkeleton } from "@/components/data-table/data-table-skeleton";
import { DateRangePicker } from "@/components/date-range-picker";
import { Shell } from "@/components/shell";

import { ProductTable } from "./_components/product-table";
import { searchParamsCache } from "./_lib/validations";
import {
  dehydrate,
  HydrationBoundary,
  QueryClient
} from "@tanstack/react-query";
import { client } from "@/lib/client";

interface IndexPageProps {
  searchParams: Promise<SearchParams>;
}

export default async function IndexPage(props: IndexPageProps) {
  const queryClient = new QueryClient();
  const searchParams = await props.searchParams;
  const search = searchParamsCache.parse(searchParams);

  const validFilters = getValidFilters(search.filters);

  await queryClient.prefetchQuery({
    queryKey: ["products"],
    queryFn: () =>
      client.getPagedListProduct(undefined, undefined, undefined, undefined)
  });

  return (
    <HydrationBoundary state={dehydrate(queryClient)}>
      <Shell className="gap-2">
        <React.Suspense fallback={<Skeleton className="h-7 w-52" />}>
          <DateRangePicker
            triggerSize="sm"
            triggerClassName="ml-auto w-56 sm:w-60"
            align="end"
            shallow={false}
          />
        </React.Suspense>
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
          <ProductTable />
        </React.Suspense>
      </Shell>
    </HydrationBoundary>
  );
}
