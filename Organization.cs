using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
//using NAudio.wave;

class Employee
{
	public static int nextId = 1;

	string title;
	string name;
	double monthlySalary;
	int id;
	public Employee child=null;
	public Employee sibling = null;
	public Employee parent=null;


	public double MonthlySalary
	{
		set { this.monthlySalary = value; }
		get { return this.monthlySalary; }
	}

	public string Name
	{
		set { this.name = value; }
		get { return this.name; }
	}

	public string Title
	{
		set { this.title = value; }
		get { return this.title; }
	}

	public int Id
	{
		set { this.id = value; }
		get { return this.id; }
	}

	public Employee()
	{

	}

	public Employee(string title, string name, double monthlySalary)
	{
		this.title = title;
		this.name = name;
		this.monthlySalary = monthlySalary;
		this.id = nextId++;
	}

	public override string ToString()
	{
		return $"Employee ID: {this.id}\nEmployee name: {this.name}\nMonthly Salary: {this.monthlySalary}\nTitle: {this.title}";
	}
}

namespace draft_project
{
	internal class Program
	{
		static public Employee FindWithId(Employee root, int id)
		{
			Employee current = root;
			while (current != null)
			{
				if (current.Id == id)
				{
					return current;
				}
				if (current.child != null)
				{
					current = current.child;
				}
				else
				{
					while (current.child == null && current.parent != null)
					{
						current = current.parent;
					}
					current = current.sibling;
				}
			}
			return null;
		}

		static void DisplayEmployeeInformation(Employee employee)
		{
			if (employee != null)
			{
				Console.WriteLine(employee.ToString());
			}
			else
			{
				Console.WriteLine("Employee not found.");
			}
		}

		static void DisplayOrganizationHierarchy(Employee root)
		{
			if (root != null)
			{
				Console.WriteLine("Organization Hierarchy:");
				DisplayHierarchy(root);
			}
			else
			{
				Console.WriteLine("No employees in the organization.");
			}
		}

		static void DisplayHierarchy(Employee employee, int indentation = 0)
		{
			if (employee != null)
			{
				Console.WriteLine((new string(' ', indentation)) + $"ID: {employee.Id} - {employee.Name}");
				DisplayHierarchy(employee.child, indentation + 2);
				DisplayHierarchy(employee.sibling, indentation);
			}
		}

		static void Main(string[] args)
		{
			Console.BackgroundColor = ConsoleColor.Red;
			
			int count = 0;
			Employee root = null;

			while (true)
			{
				Console.Clear();
				Console.WriteLine("Please choose an option");
				Console.WriteLine("1 - Hire an employee  ");
				Console.WriteLine("2 - Fire an employee ");
				Console.WriteLine("3 - Search for an Employee by ID");
				Console.WriteLine("4 - Displaying hierarchy with ID ");
				Console.WriteLine("5 - Update salary of an Employee");
				Console.WriteLine("6 - Display information of an employee");
				Console.WriteLine("7 - Find Number of employees");
				Console.WriteLine("8 - Swap 2 employees");
				Console.WriteLine("9 - Exit Program");
				Console.Write("\nYour choice : ");

				try
				{
					int option = int.Parse(Console.ReadLine());

					switch (option)
					{
						case 1:
							Console.Clear();
							Console.WriteLine("Enter the Employee's Title, Name and Monthly salary");
							Console.Write("Title : ");
							string title = Console.ReadLine();
							Console.Write("Name : ");
							string name = Console.ReadLine();
							Console.Write("Salary :");
							double salary = double.Parse(Console.ReadLine());
							Employee Adding = new Employee(title, name, salary);

							if (root == null)
							{
								root = Adding;
								Console.WriteLine("\nEmployee " + Adding.Name + " is the first Employee added!");
								System.Media.SoundPlayer player3 = new System.Media.SoundPlayer(@"C:\Users\Mohammed\Downloads\applause2.wav");
								player3.Load();
								player3.PlaySync();
								Console.WriteLine("\n   \\o/");
								Console.WriteLine("    |");
								Console.WriteLine("   / \\");
								Console.WriteLine("\nEnter any key to Proceed");
								string hh=Console.ReadLine();
								count++;
							}
							else
							{
								Console.WriteLine("Enter Employee's manager ID");
								int managerId = int.Parse(Console.ReadLine());
								Employee manager = FindWithId(root, managerId);
								if (manager != null)
								{
									if (manager.child == null)
										manager.child = Adding;
									else
									{
										Employee sibling_ = manager.child;
										while (sibling_.sibling != null)
										{
											sibling_ = sibling_.sibling;
										}
										sibling_.sibling = Adding;
									}
									Console.WriteLine("\nEmployee " + Adding.Name + " added under Manager " + manager.Name);
									System.Media.SoundPlayer player3 = new System.Media.SoundPlayer(@"C:\Users\Mohammed\Downloads\applause2.wav");
									player3.Load();
									player3.PlaySync();
									Console.WriteLine("\n   \\o/");
									Console.WriteLine("    |");
									Console.WriteLine("   / \\");

									Console.WriteLine("\nEnter any key to Proceed");
									Console.ReadKey();
									count++;
								}
								else
								{
									Console.WriteLine("\nManager with ID " + managerId + " not found");
									Employee.nextId--;
									Console.WriteLine("\nEnter any key to Proceed");
									Console.ReadKey();
								}
							}

							break;

						case 2:
							Console.Clear();
							if (root == null)
							{
								Console.WriteLine("No Employees to delete.");
								Console.WriteLine("\nEnter any key to Proceed");
								Console.ReadKey();
							}
							else
							{
								Console.WriteLine("Enter Employee ID");
								int idToBeDeleted = int.Parse(Console.ReadLine());
								Employee delete = FindWithId(root, idToBeDeleted);

								if (delete != null)
								{
									// Check if the employee has children
									if (delete.child != null)
									{
										// Reattach children to the parent or the preceding sibling
										if (delete.parent != null)
										{
											if (delete.parent.child.Id == delete.Id)
											{
												if (delete.sibling != null)
												{
													delete.sibling = delete.child;
														root = delete.parent;
													root.child = delete.sibling;
													delete = null;
												}
												else
												{ delete.parent.child = delete.child;
													root = delete.parent;
													root.child = delete.sibling;
													delete = null;
													// Employee to be deleted is the first child, reattach its children to the parent
												}
											}
											else
											{
												// Employee to be deleted is not the first child, reattach its children to the preceding sibling
												Employee previousSibling = delete.parent.child;
												while (previousSibling.sibling.Id != delete.Id)
												{
													previousSibling = previousSibling.sibling;
												}
												previousSibling.sibling = delete.child;
											}


										}
										else
										{
											// Employee to be deleted is the root, reattach its children as the new root
											root = delete.child;


										}
									}
									else
									{
										// Employee has no children, remove from parent's child or sibling
										if (delete.parent != null)
										{
											if (delete.parent.child.Id == delete.Id)
											{
												delete.parent.child.Id = delete.sibling.Id;
												delete.sibling.child = delete.child;
											}
											else
											{
												Employee previous = delete.parent.child;
												while (previous.sibling.Id != delete.Id)
												{
													previous = previous.sibling;
												}
												previous.sibling = delete.sibling;
											}
										}
										else
										{
											// Employee is the root and has no children
											root = null;
										}
										//count--; // Decrease the count for a single deleted employee
									}
									count--;
									Console.WriteLine("Employee " + delete.Name + " has been removed");
									System.Media.SoundPlayer player3 = new System.Media.SoundPlayer(@"C:\Users\Mohammed\Downloads\BOO1.wav");
									player3.Load();
									player3.PlaySync();

								}
								else
								{
									Console.WriteLine("Employee with ID " + idToBeDeleted + " not found to be deleted");
								}

								Console.WriteLine("\nEnter any key to Proceed");
								Console.ReadKey();
							}
							break;

						case 3:
							Console.Clear();
							Console.WriteLine("Enter Employee ID");
							int idToSearch = int.Parse(Console.ReadLine());
							Employee employeeFound = FindWithId(root, idToSearch);

							if (employeeFound != null)
							{
								Console.WriteLine($"Employee with ID '{idToSearch}' found:\n{employeeFound}");
							}
							else
							{
								Console.WriteLine($"Employee with ID '{idToSearch}' not found.");
							}
							Console.WriteLine("\nEnter any key to Proceed");
							Console.ReadKey();
							break;
						case 4:
							Console.Clear();
							if (root != null)
							{
								Console.WriteLine("Note That names in the same column means they are siblings,\nintendation means that the intended name is the child of the last\nname in the column(level) before\n\n");
								DisplayHierarchy(root);
							}
							else
								Console.WriteLine("Organization is empty");
							Console.WriteLine("\n\nEnter any key to Proceed");
							Console.ReadKey();
							break;
						case 5:
							Console.Clear();
							if (root == null)
							{
								Console.WriteLine("No Employees to update.");
							}
							else
							{
								Console.WriteLine("Enter Employee's ID");
								int idToUpdate = int.Parse(Console.ReadLine());
								Employee toUpdate = FindWithId(root, idToUpdate);
								if (toUpdate != null)
								{
									Console.WriteLine("What would you like to update?\n1 - Title\n2 - Name\n3 - Monthly salary");
									Console.Write("Your choice : ");

									int choice = int.Parse(Console.ReadLine()); ;

									if (choice == 1)
									{
										Console.WriteLine("Enter New Title");
										string newTitle = Console.ReadLine();
										toUpdate.Title = newTitle;
										Console.WriteLine("Title updated successfully to: " + toUpdate.Title);
									}
									else if (choice == 2)
									{
										Console.WriteLine("Enter New Name");
										string newName = Console.ReadLine();
										toUpdate.Name = newName;
										Console.WriteLine("Name updated successfully to: " + toUpdate.Name);
									}
									else if (choice == 3)
									{
										Console.WriteLine("Enter New Salary");
										double newSalary = double.Parse(Console.ReadLine());
										toUpdate.MonthlySalary = newSalary;
										Console.WriteLine("Salary updated successfully to: " + toUpdate.MonthlySalary);
									}
								}
								else
								{
									Console.WriteLine("Employee not found");

								}
							}
							Console.WriteLine("\nEnter any key to Proceed");
							Console.ReadKey();
							break;
						case 6:
							Console.Clear();
							Console.WriteLine("Enter Employee's ID");
							int idToDisplay = int.Parse(Console.ReadLine());
							Employee toDisplay = FindWithId(root, idToDisplay);
							DisplayEmployeeInformation(toDisplay);
							Console.WriteLine("\nEnter any key to Proceed");
							Console.ReadKey();
							break;
						case 7:
							Console.Clear();
							Console.WriteLine("Number of Employees in the company: " + count);
							Console.WriteLine("\nEnter any key to Proceed");
							Console.ReadKey();
							break;
						case 8:
							Console.Clear();
							if (root == null)
							{
								Console.WriteLine("List of employees is empty");
							}
							else
							{
								Console.WriteLine("Enter employee 1 ID");
								int employee1id = int.Parse(Console.ReadLine());
								Employee EMP1 = FindWithId(root, employee1id);

								Console.WriteLine("Enter employee 2 ID");
								int EMPLOYEE2ID = int.Parse(Console.ReadLine());
								Employee EMP2 = FindWithId(root, EMPLOYEE2ID);

								if (EMP2 != null && EMP1 != null)
								{
									string tempName = EMP2.Name;
									//string tempTitle = EMP2.Title;
									int tempId = EMP2.Id;
									//double tempMonthlySalary=EMP2.MonthlySalary;

									EMP2.Name = EMP1.Name;
									//EMP2.Title = EMP1.Title;
									EMP2.Id = EMP1.Id;
									//EMP2.MonthlySalary = EMP1.MonthlySalary;

									EMP1.Name = tempName;
									//EMP1.Title = tempTitle;
									EMP1.Id = tempId;
									//EMP1.MonthlySalary = tempMonthlySalary;

									Console.WriteLine("\n\nSwapped successfully");
									
								}
								else
								{
									Console.WriteLine("One or both of the employees isn't found");
									
								}
							}
							Console.WriteLine("\nEnter any key to Proceed");
							Console.ReadKey();
							break;


						case 9:
							break;
						default:
							Console.WriteLine("Invalid option. Please enter a valid option.");
							Console.WriteLine("\nEnter any key to Proceed");
							Console.ReadKey();
							break;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("An error occurred: " + ex.Message);
					
					Console.WriteLine("\nEnter any key to Proceed");
					Console.ReadKey();

				}
			}
		}
	}
}
