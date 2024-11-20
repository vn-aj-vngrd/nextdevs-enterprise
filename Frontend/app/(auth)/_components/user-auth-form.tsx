"use client";

import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { authenticate } from "@/server/account";
import { zodResolver } from "@hookform/resolvers/zod";
import { signIn } from "next-auth/react";
import { useSearchParams } from "next/navigation";
import { useTransition } from "react";
import { useForm } from "react-hook-form";
import * as z from "zod";

const formSchema = z.object({
  username: z
    .string()
    .min(3, { message: "Username must be at least 3 characters" }),
  password: z
    .string()
    .min(6, { message: "Password must be at least 6 characters" })
});

type UserFormValue = z.infer<typeof formSchema>;

export default function UserAuthForm() {
  const searchParams = useSearchParams();
  const callbackUrl = searchParams.get("callbackUrl");
  const [loading, startTransition] = useTransition();

  const form = useForm<UserFormValue>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      username: "",
      password: ""
    }
  });

  const onSubmit = async (data: UserFormValue) => {
    startTransition(() => {
      signIn("credentials", {
        username: data.username,
        password: data.password,
        callbackUrl: callbackUrl ?? "/dashboard"
      });
    });
    // const res = await authenticate({
    //   userName: data.username,
    //   password: data.password
    // });

    // console.log(res);
  };

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className="grid w-full gap-4"
      >
        <FormField
          control={form.control}
          name="username"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Username</FormLabel>
              <FormControl>
                <Input
                  type="string"
                  placeholder="Enter your username"
                  disabled={loading}
                  {...field}
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />

        <FormField
          control={form.control}
          name="password"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Password</FormLabel>
              <FormControl>
                <Input
                  type="password"
                  placeholder="Enter your password"
                  disabled={loading}
                  {...field}
                />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />

        <Button disabled={loading} className="ml-auto w-full" type="submit">
          {loading ? "Signing In..." : "Sign In"}
        </Button>
      </form>
    </Form>
  );
}
