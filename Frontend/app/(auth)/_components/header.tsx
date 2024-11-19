"use client";

import Image from "next/image";
import { useTheme } from "next-themes";

export default function Header() {
  const { resolvedTheme } = useTheme();

  const src =
    resolvedTheme === "dark"
      ? "/assets/branding/SVG/next_devs_logo.svg"
      : "/assets/branding/SVG/next_devs_logo_dark.svg";

  return (
    <>
      <Image src={src} alt="NextAdmin" width={40} height={40} />
    </>
  );
}
