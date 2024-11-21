"use client";

import * as React from "react";
import { type DataTableRowAction } from "@/types";
import { type ColumnDef } from "@tanstack/react-table";
import dayjs from "dayjs";

import { Checkbox } from "@/components/ui/checkbox";

import { ProductDto } from "@/lib/api-client";
import { DataTableColumnHeader } from "@/components/data-table/data-table-column-header";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuSeparator,
  DropdownMenuShortcut,
  DropdownMenuTrigger
} from "@/components/ui/dropdown-menu";
import { Ellipsis } from "lucide-react";
import { Button } from "@/components/ui/button";

interface GetColumnsProps {
  setRowAction: React.Dispatch<
    React.SetStateAction<DataTableRowAction<ProductDto> | null>
  >;
}

export function getColumns({
  setRowAction
}: GetColumnsProps): ColumnDef<ProductDto>[] {
  return [
    {
      id: "name",
      accessorKey: "name",
      header: ({ table, column }) => (
        <div className="flex items-center justify-start gap-8">
          <Checkbox
            checked={
              table.getIsAllPageRowsSelected() ||
              (table.getIsSomePageRowsSelected() && "indeterminate")
            }
            onCheckedChange={(value) =>
              table.toggleAllPageRowsSelected(!!value)
            }
            aria-label="Select all"
          />
          <DataTableColumnHeader column={column} title="NAME" />
        </div>
      ),
      cell: ({ row }) => (
        <div className="flex items-center gap-8">
          <Checkbox
            checked={row.getIsSelected()}
            onCheckedChange={(value) => row.toggleSelected(!!value)}
            aria-label="Select row"
          />
          <div>{row.getValue("name")}</div>
        </div>
      )
    },
    {
      accessorKey: "price",
      header: ({ column }) => (
        <DataTableColumnHeader column={column} title="PRICE" />
      ),
      cell: ({ row }) => <div>{row.getValue("price")}</div>
    },
    {
      accessorKey: "barCode",
      header: ({ column }) => (
        <DataTableColumnHeader column={column} title="BAR CODE" />
      ),
      cell: ({ row }) => <div>{row.getValue("barCode")}</div>
    },
    {
      accessorKey: "createdDateTime",
      header: ({ column }) => (
        <DataTableColumnHeader column={column} title="CREATED DATE" />
      ),
      cell: ({ row }) => (
        <div>
          {dayjs(row.getValue("createdDateTime")).format("YYYY-MM-DD HH:mm:ss")}
        </div>
      )
    },
    {
      id: "actions",
      cell: function Cell({ row }) {
        const [isUpdatePending, startUpdateTransition] = React.useTransition();

        return (
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button
                aria-label="Open menu"
                variant="ghost"
                className="flex size-8 p-0 data-[state=open]:bg-muted"
              >
                <Ellipsis className="size-4" aria-hidden="true" />
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end" className="w-40">
              <DropdownMenuItem
                onSelect={() => setRowAction({ row, type: "update" })}
              >
                Edit
              </DropdownMenuItem>

              <DropdownMenuSeparator />
              <DropdownMenuItem
                onSelect={() => setRowAction({ row, type: "delete" })}
              >
                Delete
                <DropdownMenuShortcut>⌘⌫</DropdownMenuShortcut>
              </DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        );
      },
      size: 40
    }
  ];
}
