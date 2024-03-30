using Bb.Extensions;
using Bb.OpenApi;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Black.Beard.OpenApiUnitTest
{


    // https://github.com/gregsdennis/json-everything/tree/master

    [TestClass]
    public class UnitTest1
    {
        private DirectoryInfo? _directory;

        public UnitTest1()
        {

            var assembly = Assembly.GetEntryAssembly();
            _directory = new FileInfo(assembly.Location).Directory;

        }


        [TestMethod]
        public void TestGenerateOpenApi()
        {

            var file1 = Path.Combine(_directory.FullName, "test1.json");
            var t1 = typeof(ModelTest1);
            var ctx = t1.GenerateOpenApiContract()
                        .ApplyDocumentation(LoadDocumentationExtension.LoadXmlFiles())
                ;

            OpenApiDocument contract = ctx.Generate();
            contract.SaveOpenApiContract(file1);

        }

        [TestMethod]
        public void TestGenerateSchema()
        {

            var file1 = Path.Combine(_directory.FullName, "test2.json");

            var t1 = typeof(ModelTest1);
            var t2 = typeof(ModelTest2);
            var ctx = t1.GenerateOpenApiContract()
                        .GenerateOpenApiContract(t2)
                        .ApplyDocumentation(LoadDocumentationExtension.LoadXmlFiles())
                        ;

            OpenApiDocument contract = ctx.Generate();

            var schema1 = contract.GenerateSchemaContract("ModelTest1", "http://local/model/modeltest1");
            var schema2 = contract.GenerateSchemaMultiContract(new string[] { "ModelTest1", "ModelTest2" }, "http://local/model/modeltest1");

        }

        [TestMethod]
        public void TestMethod2()
        {

            var file = Path.Combine(_directory.FullName, "Models", "ModelTest1.json");

            OpenApiDocument contract = OpenApiExtension.LoadOpenApiContract(@"D:\\srcs_externe\\puscontract\\Contrat_pus\\contract.json");
            //var contractParcel1 = contract.GenerateSchemaContract("ParcelList", "http://pickup-services/parcel/ParcelList");
            //var txtParcel1 = contractParcel1.ToJsonString(new System.Text.Json.JsonSerializerOptions() { WriteIndented = true });
            ////File.WriteAllText(@"D:\srcs\puscontract\Contrat_pus\ContractParcel.json", txtParcel);



            var definitions = contract.GetSchemaObjects("Parcel", out var id, out var properties);





            var contractSchema = OpenApiExtension.GenerateSchemaContract("", "", definitions, c =>
            {
                c["type"] = "array";
                c["maxItems"] = "";
                c["items"] = "";
                c["description"] = "";
                // c["property"] = properties.Value;

            });



            var txtParcel2 = contractSchema.ToJsonString(new System.Text.Json.JsonSerializerOptions() { WriteIndented = true });



            //var txtParcel2 = contractParcel2.ToJsonString(new System.Text.Json.JsonSerializerOptions() { WriteIndented = true });
            ////File.WriteAllText(@"D:\srcs\puscontract\Contrat_pus\ContractParcel.json", txtParcel);


            //var contractEvent = contract.GenerateSchemaContract("TrackingList", "http://pickup-services/parcel/TrackingList");
            //var txtEvents = contractEvent.ToJsonString(new System.Text.Json.JsonSerializerOptions() { WriteIndented = true });
            //File.WriteAllText(@"D:\srcs\puscontract\Contrat_pus\ContractTrackingList.json", txtEvents);

        }



        public class ModelTest1 : ModelTestBase
        {

            /// <summary>
            /// Kind of human
            /// </summary>
            public KindEnum Kind { get; set; }

            /// <summary>
            /// Name of the human
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Age of the human
            /// </summary>
            public int Age { get; set; }


        }

        /// <summary>
        /// Test model 2
        /// </summary>
        public class ModelTest2 : ModelTestBase
        {

            /// <summary>
            /// Property 1
            /// </summary>
            public string Property1 { get; set; }


        }

    }

    /// <summary>
    /// Description for Model test base
    /// </summary>
    public class ModelTestBase
    {

        /// <summary>
        /// Parent name
        /// </summary>
        public string ParentName { get; set; }

    }

    /// <summary>
    /// Kind of human
    /// </summary>
    //[JsonConverter(typeof(_EnumStringConverter<KindEnum>))]
    public enum KindEnum
    {

        /// <summary>
        /// Sexe male
        /// </summary>
        Male,

        /// <summary>
        /// Sexe female
        /// </summary>
        Female,
    }


}