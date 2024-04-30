using Persistance.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Services
{
    public class DataService
    {
        private List<string> Colors = new List<string>();
        private List<string> Diameters = new List<string>();
        private List<string> Lengths = new List<string>();
        private List<string> Materials = new List<string>();
        PersistanceContext _context { get; set; }
        CommandService _commandService { get; set; }

        public DataService(PersistanceContext context, CommandService commandService)
        {
            _context = context;
            _commandService = commandService;
        }

        private async Task ClearTablesAsync()
        {
            _context.Color.RemoveRange(_context.Color);
            _context.Diameter.RemoveRange(_context.Diameter);
            _context.Wire.RemoveRange(_context.Wire);
            _context.Length.RemoveRange(_context.Length);
            _context.Material.RemoveRange(_context.Material);

            await _context.SaveChangesAsync();


        }

        private async Task ResetIdentityInTables()
        {
            await _commandService.ExecuteCommandAsync("DBCC CHECKIDENT ('Color', RESEED, 0);");
            await _commandService.ExecuteCommandAsync("DBCC CHECKIDENT ('Material', RESEED, 0);");
            await _commandService.ExecuteCommandAsync("DBCC CHECKIDENT ('Length', RESEED, 0);");
            await _commandService.ExecuteCommandAsync("DBCC CHECKIDENT ('Diameter', RESEED, 0);");
        }

        public async Task InitialDataAsync()
        {
            await ClearTablesAsync();
            await ResetIdentityInTables();
            RefillPosibleValues();
            var random = new Random();

            for (int i = 0; i < 6; i++)
            {
                var targetNumber = random.Next(20, 50);
                var randomWire = GetRandomWire(targetNumber);

                _context.Color.Add(randomWire.Color);
                _context.Diameter.Add(randomWire.Diameter);
                _context.Length.Add(randomWire.Length);
                _context.Material.Add(randomWire.Material);
            }

            await _context.SaveChangesAsync();       
        }

        private void RefillPosibleValues()
        {
            Colors = new List<string>()
             { "Green", "Red", "Blue","Yellow", "Black", "White", "Brown", "Purple", "Pink"};
            Diameters = new List<string>()
             { "1mm", "1.5mm", "2mm", "2.5mm", "3mm", "3.5mm", "4mm", "4.5mm", "5mm"  };
            Lengths = new List<string>()
            { "3cm", "4cm", "5cm", "6cm", "7cm", "8cm", "9cm", "10cm", "11cm", "12cm"};
            Materials = new List<string>()
            {
                "Material1", "Material2",  "Material3" , "Material4",  "Material5",  "Material6"
            };
        }

        private Wire GetRandomWire(int targetNumber)
        {
            var numbers = SuccessSumNumber(targetNumber);
            var random = new Random();
            var randomDiameter = Diameters.ElementAt(random.Next(Diameters.Count));
            Diameters.Remove(randomDiameter);
            var diameter = new Diameter()
            {
                Value = randomDiameter,
                Hash = numbers[0]
            };
            
            var randomLength = Lengths.ElementAt(random.Next(Lengths.Count));
            Lengths.Remove(randomLength);

            var length = new Length()
            {
                Value = randomLength,
                Hash = numbers[1]
            };
            var randomColor = Colors.ElementAt(random.Next(Colors.Count));
            Colors.Remove(randomColor);

            var color = new Color()
            {
                Value = randomColor,
                Hash = numbers[2]
            };

            var randomMaterial = Materials.ElementAt(random.Next(Materials.Count));
            Materials.Remove(randomMaterial);

            var material = new Material()
            {
                Value = randomMaterial,
                Hash = numbers[3]
            };

            return new Wire()
            {
                Material = material,
                Color = color,
                Length = length,
                Diameter = diameter
            };
        }

        private List<int> SuccessSumNumber(int targetNumber)
        {
            var currentNumber = targetNumber;
            var result = new List<int>();
            var random = new Random();
            for (var i = 0; i < 4; i++)
            {
                if (i == 3)
                {
                    result.Add(targetNumber - result.Sum());
                }
                else
                {
                    var topNumber = (int)double.Round(currentNumber / 1.5);
                    
                    var randomNumber = random.Next(topNumber);
                    while(result.Contains(randomNumber))
                    {
                        randomNumber = random.Next(topNumber);
                    }
                    currentNumber -= randomNumber;
                    result.Add(randomNumber);
                }
            }

            return result;
        }
    }
}
