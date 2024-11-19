"use client";

import * as React from "react";
import { cn } from "@/lib/utils";

// Define the spinner styles as a constant for better readability and maintainability
const spinnerStyles = "border-4 border-t-4 rounded-full animate-spin";

// Define the default colors for the spinner
const defaultSpinnerColors = {
  border: "gray-200",
  borderTop: "gray-900",
  size: "w-16 h-16"
};

interface LoadingSpinnerProps extends React.HTMLAttributes<HTMLDivElement> {
  className?: string;
  borderColor?: string;
  borderTopColor?: string;
  size?: number | string;
}

/**
 * A simple loading spinner component.
 *
 * @param props - The props for the component.
 * @returns The loading spinner element.
 */
const LoadingSpinner = React.forwardRef<HTMLDivElement, LoadingSpinnerProps>(
  (props, ref) => {
    const { className, borderColor, borderTopColor, size, ...rest } = props;

    const borderStyle = cn(
      `${spinnerStyles} border-${borderColor || defaultSpinnerColors.border} border-t-${
        borderTopColor || defaultSpinnerColors.borderTop
      }`
    );

    const sizeStyle = size ? `h-${size} w-${size}` : defaultSpinnerColors.size;

    return (
      <div className={cn(sizeStyle, className)} ref={ref} {...rest}>
        <div className={cn(borderStyle, sizeStyle)} />
      </div>
    );
  }
);

LoadingSpinner.displayName = "LoadingSpinner";

export { LoadingSpinner };
