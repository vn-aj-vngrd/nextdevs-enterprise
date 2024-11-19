import { Metadata } from "next";
import EmployeeViewPage from "../_components/employee-view-page";

export const metadat: Metadata = {
  title: "Employee | NextDevs Inc."
};

export default function Page() {
  return <EmployeeViewPage />;
}
