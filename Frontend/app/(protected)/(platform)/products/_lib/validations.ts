import {
  createSearchParamsCache,
  parseAsArrayOf,
  parseAsInteger,
  parseAsString,
  parseAsStringEnum
} from "nuqs/server";
import * as z from "zod";

import { getFiltersStateParser, getSortingStateParser } from "@/lib/parsers";
import { ProductDto } from "@/lib/api-client";

export const searchParamsCache = createSearchParamsCache({
  flags: parseAsArrayOf(z.enum(["advancedTable", "floatingBar"])).withDefault(
    []
  ),
  name: parseAsString.withDefault(""),
  pageNumber: parseAsInteger.withDefault(1),
  pageSize: parseAsInteger.withDefault(10),
  sort: getSortingStateParser<ProductDto>().withDefault([
    { id: "name", desc: true }
  ]),
  from: parseAsString.withDefault(""),
  to: parseAsString.withDefault(""),

  // advanced filter
  filters: getFiltersStateParser().withDefault([]),
  joinOperator: parseAsStringEnum(["and", "or"]).withDefault("and")
});

// export const createTaskSchema = z.object({
//   title: z.string(),
//   label: z.enum(tasks.label.enumValues),
//   status: z.enum(tasks.status.enumValues),
//   priority: z.enum(tasks.priority.enumValues),
// })

// export const updateTaskSchema = z.object({
//   title: z.string().optional(),
//   label: z.enum(tasks.label.enumValues).optional(),
//   status: z.enum(tasks.status.enumValues).optional(),
//   priority: z.enum(tasks.priority.enumValues).optional(),
// })

export type GetTasksSchema = Awaited<
  ReturnType<typeof searchParamsCache.parse>
>;
// export type CreateTaskSchema = z.infer<typeof createTaskSchema>
// export type UpdateTaskSchema = z.infer<typeof updateTaskSchema>
