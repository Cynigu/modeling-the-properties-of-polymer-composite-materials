using Polimer.App.ViewModel.Technology.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Polimer.App.ViewModel.Admin.Models;
using Xceed.Document.NET;
using Xceed.Words.NET;
using System.Text.RegularExpressions;

namespace Polimer.App.Services;

public class ReportService : IFileService
{
    private const string WordTemplate = "ReportTemplate.docx";

    private static string ReplaceFunc(string findStr, Dictionary<string, string> _replacePatterns)
    {
        if (_replacePatterns.ContainsKey(findStr))
        {
            return _replacePatterns[findStr];
        }
        return findStr;
    }

    public void Save(string filename, ComputeRecipeParametersModel data, ComputeRecipeParametersModel? dataI = null)
    {
        //Xceed.Document.NET.Licenser.LicenseKey = "";
        using (var document = DocX.Load(WordTemplate))
        {
            InputDataTable(data, document);

            // таблица входных данных
            OutputDataTable(data, document, dataI);


            var _replacePatterns = new Dictionary<string, string>()
            {
                { "DATE", DateTime.Now.ToString(CultureInfo.InvariantCulture) },
                { "NAME_P1", data.Recipe.CompatibilityMaterial.FirstMaterial.QuickName },
                { "NAME_P2",  data.Recipe.CompatibilityMaterial.SecondMaterial.QuickName },
                { "NAME_P3", data.Recipe.Additive.QuickName },
                { "V_P1", data.Recipe.ContentMaterialFirst.ToString(CultureInfo.InvariantCulture) },
                { "V_P2", data.Recipe.ContentMaterialSecond.ToString(CultureInfo.InvariantCulture) },
                { "V_P3", data.Recipe.ContentAdditive.ToString(CultureInfo.InvariantCulture) },
            };

            if (dataI !=null)
            {
                _replacePatterns.Add("VI_P1", dataI.Recipe.ContentMaterialFirst.ToString(CultureInfo.InvariantCulture));
                _replacePatterns.Add("VI_P2", dataI.Recipe.ContentMaterialSecond.ToString(CultureInfo.InvariantCulture));
                _replacePatterns.Add("VI_P3", dataI.Recipe.ContentAdditive.ToString(CultureInfo.InvariantCulture));
            }

            var replaceTextOptions = new FunctionReplaceTextOptions()
            {
                FindPattern = "%(.*?)%",
                RegExOptions = RegexOptions.IgnoreCase,
                RegexMatchHandler = (str) => ReplaceFunc(str, _replacePatterns),
                //RegExOptions = RegexOptions.IgnoreCase,
                //NewFormatting = new Formatting() { Bold = true, FontColor = System.Drawing.Color.Green }
            };

            document.ReplaceText(replaceTextOptions);

            document.SaveAs(filename);
        }

    }

    private static void OutputDataTable(ComputeRecipeParametersModel data, DocX document, ComputeRecipeParametersModel? dataI = null)
    {
        var outputListTable = document.Tables.FirstOrDefault(t => t.TableCaption == "OUTPUT_LIST");
        if (outputListTable == null)
        {
            throw new ArgumentException("Error, couldn't find table with caption GROCERY_LIST in current document.");
        }

        if (outputListTable.RowCount > 1)
        {
            // Get the row pattern of the second row.
            var rowPattern = outputListTable.Rows[1];

            // Add items (rows) to the grocery list.

            AddIOutPutDataItemToTable(outputListTable, rowPattern, data);


            // Remove the pattern row.
            rowPattern.Remove();
        }

        var resaerchTableList = document.Tables.FirstOrDefault(t => t.TableCaption == "RESEARCH_LIST");
        if (resaerchTableList == null)
        {
            throw new ArgumentException("Error, couldn't find table with caption GROCERY_LIST in current document.");
        }

        if (resaerchTableList.RowCount > 1)
        {
            // Get the row pattern of the second row.
            var rowPattern = resaerchTableList.Rows[1];

            // Add items (rows) to the grocery list.

            AddIOutPutDataIItemToTable(resaerchTableList, rowPattern, dataI);


            // Remove the pattern row.
            rowPattern.Remove();
        }
    }

    private static void InputDataTable(ComputeRecipeParametersModel data, DocX document)
    {
        // таблица входных данных
        var inputListTable = document.Tables.FirstOrDefault(t => t.TableCaption == "INPUT_LIST");
        if (inputListTable == null)
        {
            throw new ArgumentException("Error, couldn't find table with caption GROCERY_LIST in current document.");
        }

        if (inputListTable.RowCount > 1)
        {
            // Get the row pattern of the second row.
            var rowPattern = inputListTable.Rows[1];

            // Add items (rows) to the grocery list.
            AddInputDataItemToTable(inputListTable, rowPattern, data.Recipe, data.UsefulProduct);

            // Remove the pattern row.
            rowPattern.Remove();
        }
    }

    private static void AddIOutPutDataItemToTable(Table table, Row rowPattern, ComputeRecipeParametersModel data)
    {
        // Insert a copy of the rowPattern at the last index in the table.
        var newItem = table.InsertRow(rowPattern, table.RowCount - 1);

        // Replace the default values of the newly inserted row.
        newItem.ReplaceText(new StringReplaceTextOptions() { SearchValue = "%OPP%", NewValue = data.Density.ToString(CultureInfo.InvariantCulture) });
        newItem.ReplaceText(new StringReplaceTextOptions() { SearchValue = "%OPV%", NewValue = data.Viscosity.ToString(CultureInfo.InvariantCulture) });
        newItem.ReplaceText(new StringReplaceTextOptions() { SearchValue = "%IPC%", NewValue = data.NumberOfPhases.ToString(CultureInfo.InvariantCulture) });
        newItem.ReplaceText(new StringReplaceTextOptions() { SearchValue = "%IPR%", NewValue = data.Solubility.ToString(CultureInfo.InvariantCulture) });
        newItem.ReplaceText(new StringReplaceTextOptions() { SearchValue = "%IPN%", NewValue = data.NasDensity.ToString(CultureInfo.InvariantCulture)});
        newItem.ReplaceText(new StringReplaceTextOptions() { SearchValue = "%IPPTR%", NewValue = data.Ptr.ToString(CultureInfo.InvariantCulture) });

    }

    private static void AddIOutPutDataIItemToTable(Table table, Row rowPattern, ComputeRecipeParametersModel? data)
    {
        // Insert a copy of the rowPattern at the last index in the table.
        var newItem = table.InsertRow(rowPattern, table.RowCount - 1);

        // Replace the default values of the newly inserted row.
        newItem.ReplaceText(new StringReplaceTextOptions() { SearchValue = "%OPPI%", NewValue = data?.Density.ToString(CultureInfo.InvariantCulture) ?? "" });
        newItem.ReplaceText(new StringReplaceTextOptions() { SearchValue = "%OPVI%", NewValue = data?.Viscosity.ToString(CultureInfo.InvariantCulture) ?? "" });
        newItem.ReplaceText(new StringReplaceTextOptions() { SearchValue = "%IPCI%", NewValue = data?.NumberOfPhases.ToString(CultureInfo.InvariantCulture) ?? "" });
        newItem.ReplaceText(new StringReplaceTextOptions() { SearchValue = "%IPRI%", NewValue = data?.Solubility.ToString(CultureInfo.InvariantCulture) ?? "" });
        newItem.ReplaceText(new StringReplaceTextOptions() { SearchValue = "%IPNI%", NewValue = data?.NasDensity.ToString(CultureInfo.InvariantCulture) ?? "" });
        newItem.ReplaceText(new StringReplaceTextOptions() { SearchValue = "%IPPTRI%", NewValue = data?.Ptr.ToString(CultureInfo.InvariantCulture) ?? "" });

    }

    private static void AddInputDataItemToTable(Table table, Row rowPattern, RecipeModel recipe, UsefulProductModel usefulProduct)
    {
        // Insert a copy of the rowPattern at the last index in the table.
        var newItem = table.InsertRow(rowPattern, table.RowCount - 1);

        // Replace the default values of the newly inserted row.
        newItem.ReplaceText(new StringReplaceTextOptions() { SearchValue = "%IP1%", NewValue = recipe.CompatibilityMaterial.FirstMaterial.QuickName });
        newItem.ReplaceText(new StringReplaceTextOptions() { SearchValue = "%IP2%", NewValue = recipe.CompatibilityMaterial.SecondMaterial.QuickName });
        newItem.ReplaceText(new StringReplaceTextOptions() { SearchValue = "%IP3%", NewValue = recipe.Additive.QuickName });
        newItem.ReplaceText(new StringReplaceTextOptions() { SearchValue = "%IP4%", NewValue = usefulProduct.Name });
    }
}