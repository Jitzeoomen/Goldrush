using System;
using Domain;

namespace Process {
    public class MapLoader {
        private Game _game;
        private Railroad _railroad;

        public MapLoader(Game game) {
            _game = game;
            _railroad = _game.Railroad;

            CreateMap();
            LinkMap();
        }

        // Creates a new Warehouse object, and passes the reference to the Railroad.
        private Warehouse Warehouse() {
            Warehouse warehouse = new Warehouse();
            _railroad.AddWarehouse(warehouse);
            return warehouse;
        }

        // Gets the Merge object from the 2D array, and passes the reference to the Railroad.
        private void LinkMerge(int x, int y) {
            Merge merge = (Merge)_railroad.Field2D[x, y];
            merge.Next = _railroad.Field2D[x, y + 1];           // Connect the Next Rails.
            merge.PreviousTop = _railroad.Field2D[x - 1, y];    // Connect the Rails to the Top before this Merge.
            merge.PreviousBottom = _railroad.Field2D[x + 1, y]; // Connect the Rails to the Bottom before this Merge.
            merge.Previous = merge.PreviousTop;                 // Default points upwards.
            _railroad.AddSwitch(merge);
        }

        // Gets the Split object from the 2D array, and passes the reference to the Railroad.
        private void LinkSplit(int x, int y) {
            Split split = (Split)_railroad.Field2D[x, y];
            split.NextTop = _railroad.Field2D[x - 1, y];        // Connect the Rails to the Top.
            split.NextBottom = _railroad.Field2D[x + 1, y];     // Connect the Rails to the Bottom.
            split.Next = split.NextTop;                         // Default points upwards.
            _railroad.AddSwitch(split);
        }

        private Switchyard SwYard() {
            return new Switchyard();
        }

        public void CreateMap() {
            _railroad.Field2D = new Field[,] {
				{new Rails()	 , new Rails(), new Rails(), new Rails(), new Rails(), new Rails()	, new Rails(), new Rails(),	new Rails()	, new Dock() , new Rails(), new Rails()}, 
				{null			 , null		  , null	   , null		, null		 , null			, null		 , null		  , null		, null		 , null	      ,	new Rails()},
				{Warehouse()     , new Rails(), new Rails(), new Rails(), null		 , new Rails()	, new Rails(), new Rails(),	new Rails()	, new Rails(), null		  , new Rails()},
				{null			 , null		  , null	   , new Merge(), new Rails(), new Split()	, null		 , null		  , null		, new Merge(), new Rails(), new Rails()},
				{Warehouse()     , new Rails(), new Rails(), new Rails(), null		 , new Rails()	, new Rails(), null		  , new Rails()	, new Rails(), null		  ,	null	   },
				{null			 , null		  , null	   , null		, null		 , null			, new Merge(), new Rails(), new Split()	, null		 , null	      ,	null       },
				{Warehouse()     , new Rails(), new Rails(), new Rails(), new Rails(), new Rails()	, new Rails(), null		  ,	new Rails()	, new Rails(), new Rails(), new Rails()},
				{null			 , SwYard()   , SwYard()   , SwYard()   , SwYard()   , SwYard()     , SwYard()   , SwYard()   ,	SwYard()	, new Rails(), new Rails(), new Rails()},
			};
        }

        public void LinkMap() {
            // Link all 5 switches.
            LinkMerge(3, 3);
            LinkSplit(3, 5);
            LinkMerge(5, 6);
            LinkSplit(5, 8);
            LinkMerge(3, 9);

            // Link Dock to the Canal.
            Dock dock = (Dock)_railroad.Field2D[0, 9];
            dock.Canal = _game.Canal;

            //link the upper row direction to the left
            for (int i = 1; i < _railroad.Field2D.GetLength(1); i++) {
                if (_railroad.Field2D[0, i] != null) {
                    _railroad.Field2D[0, i].Next = _railroad.Field2D[0, i - 1];
                }
            }

            //link the lower row direction to the left
            for (int i = 2; i < _railroad.Field2D.GetLength(1); i++) {
                if (_railroad.Field2D[7, i] != null) {
                    _railroad.Field2D[7, i].Next = _railroad.Field2D[7, i - 1];
                }
            }

            //link warehouse a/b to merge
            for (int i = 0; i < 4; i++) {
                if (_railroad.Field2D[4, i] != null) {
                    _railroad.Field2D[4, i].Next = _railroad.Field2D[4, i + 1];
                }
                if (_railroad.Field2D[2, i] != null) {
                    _railroad.Field2D[2, i].Next = _railroad.Field2D[2, i + 1];
                }
            }

            //first merge a/b
            _railroad.Field2D[2, 3].Next = _railroad.Field2D[3, 3];
            _railroad.Field2D[4, 3].Next = _railroad.Field2D[3, 3];

            for (int i = 0; i < 2; i++) {
                _railroad.Field2D[3, 3 + i].Next = _railroad.Field2D[3, 3 + 1 + i];
            }
            for (int i = 0; i < 4; i++) {
                _railroad.Field2D[2, 5 + i].Next = _railroad.Field2D[2, 5 + i + 1];
            }

            _railroad.Field2D[4, 5].Next = _railroad.Field2D[4, 6];
            for (int i = 0; i < 6; i++) {
                if (_railroad.Field2D[6, i] != null) {
                    _railroad.Field2D[6, i].Next = _railroad.Field2D[6, i + 1];
                }
            }

            //second merge b/c
            _railroad.Field2D[4, 6].Next = _railroad.Field2D[5, 6];
            _railroad.Field2D[6, 6].Next = _railroad.Field2D[5, 6];

            for (int i = 0; i < 2; i++) {
                _railroad.Field2D[5, 6 + i].Next = _railroad.Field2D[5, 6 + 1 + i];
            }
            _railroad.Field2D[6, 8].Next = _railroad.Field2D[6, 9];

            for (int i = 0; i < 3; i++) {
                _railroad.Field2D[6, 8 + i].Next = _railroad.Field2D[6, 8 + 1 + i];
            }

            _railroad.Field2D[6, 11].Next = _railroad.Field2D[7, 11];
            _railroad.Field2D[4, 8].Next = _railroad.Field2D[4, 9];

            //3rd merge a/b
            _railroad.Field2D[2, 9].Next = _railroad.Field2D[3, 9];
            _railroad.Field2D[4, 9].Next = _railroad.Field2D[3, 9];
            _railroad.Field2D[3, 10].Next = _railroad.Field2D[3, 11];

            for (int i = 0; i < 3; i++) {
                _railroad.Field2D[3 - i, 11].Next = _railroad.Field2D[3 - i - 1, 11];
            }
        }
    }
}