"use client";

import * as React from "react";
import type {
  DataTableAdvancedFilterField,
  DataTableFilterField,
  DataTableRowAction
} from "@/types";

import { useDataTable } from "@/hooks/use-data-table";
import { DataTable } from "@/components/data-table/data-table";

import {
  ProductDto,
  ProductDtoFilterCriterion,
  ProductDtoSortCriterion
} from "@/lib/api-client";
import { getColumns } from "./product-table-columns";
import { useQuery } from "@tanstack/react-query";
import { client } from "@/lib/client";
import { ProductTableFloatingBar } from "./product-table-floating-bar";
import { DataTableAdvancedToolbar } from "@/components/data-table/data-table-advanced-toolbar";
import { DataTableToolbar } from "@/components/data-table/data-table-toolbar";
import { ProductTableToolbarActions } from "./product-table-toolbar-actions";

interface ProductTableProps {
  name?: string;
  pageNumber?: number;
  pageSize?: number;
  sortCriteria: ProductDtoSortCriterion[];
  filters: ProductDtoFilterCriterion[];
}

export function ProductTable({
  name,
  pageNumber,
  pageSize,
  sortCriteria,
  filters
}: ProductTableProps) {
  const { data } = useQuery({
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

  const [rowAction, setRowAction] =
    React.useState<DataTableRowAction<ProductDto> | null>(null);

  const columns = React.useMemo(
    () => getColumns({ setRowAction }),
    [setRowAction]
  );

  /**
   * This component can render either a faceted filter or a search filter based on the `options` prop.
   *
   * @prop options - An array of objects, each representing a filter option. If provided, a faceted filter is rendered. If not, a search filter is rendered.
   *
   * Each `option` object has the following properties:
   * @prop {string} label - The label for the filter option.
   * @prop {string} value - The value for the filter option.
   * @prop {React.ReactNode} [icon] - An optional icon to display next to the label.
   * @prop {boolean} [withCount] - An optional boolean to display the count of the filter option.
   */
  const filterFields: DataTableFilterField<ProductDto>[] = [
    {
      id: "name",
      label: "Name",
      placeholder: "Search by name"
    }
  ];

  /**
   * Advanced filter fields for the data table.
   * These fields provide more complex filtering options compared to the regular filterFields.
   *
   * Key differences from regular filterFields:
   * 1. More field types: Includes 'text', 'multi-select', 'date', and 'boolean'.
   * 2. Enhanced flexibility: Allows for more precise and varied filtering options.
   * 3. Used with DataTableAdvancedToolbar: Enables a more sophisticated filtering UI.
   * 4. Date and boolean types: Adds support for filtering by date ranges and boolean values.
   */
  const advancedFilterFields: DataTableAdvancedFilterField<ProductDto>[] = [];

  const enableAdvancedTable = false;

  const { table } = useDataTable({
    data: data?.data ?? [],
    columns,
    pageCount: data?.totalPages ?? 0,
    filterFields,
    enableAdvancedFilter: enableAdvancedTable,
    initialState: {
      columnPinning: { right: ["actions"] }
    },
    getRowId: (originalRow, index) => `${originalRow.id}-${index}`,
    shallow: false,
    clearOnDefault: true
  });

  return (
    <>
      <DataTable
        table={table}
        floatingBar={<ProductTableFloatingBar table={table} />}
      >
        {enableAdvancedTable ? (
          <DataTableAdvancedToolbar
            table={table}
            filterFields={advancedFilterFields}
            shallow={false}
          >
            <ProductTableToolbarActions table={table} />
          </DataTableAdvancedToolbar>
        ) : (
          <DataTableToolbar table={table} filterFields={filterFields}>
            <ProductTableToolbarActions table={table} />
          </DataTableToolbar>
        )}
      </DataTable>
      {/* <UpdateTaskSheet
        open={rowAction?.type === "update"}
        onOpenChange={() => setRowAction(null)}
        task={rowAction?.row.original ?? null}
      />
      <DeleteTasksDialog
        open={rowAction?.type === "delete"}
        onOpenChange={() => setRowAction(null)}
        tasks={rowAction?.row.original ? [rowAction?.row.original] : []}
        showTrigger={false}
        onSuccess={() => rowAction?.row.toggleSelected(false)}
      /> */}
    </>
  );
}
